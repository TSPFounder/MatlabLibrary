using System;
using System.Collections.Generic;
using System.Globalization;
using MLApp;

namespace MatlabLib
{
    public class SimulinkModelBuilder
    {
        //  *****************************************************************************************
        //  DECLARATIONS
        //
        //  ************************************************************
        #region
        //  
        //  Identification

        //
        //  Data

        //
        //  Owned & Owning Objects

        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  ENUMERATIONS
        //
        //  ************************************************************
        #region
        //
        //  Solver Type
        public enum SolverType
        {
            VariableStep = 0,
            FixedStep
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  SIMULINKMODELBUILDER CONSTRUCTOR
        //
        //  ************************************************************
        #region
        public SimulinkModelBuilder(MatlabApp matlabApp)
        {
            MyMatlabApp = matlabApp;
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  PROPERTIES
        //
        //  ************************************************************
        #region
        //  
        //  Owned & Owning Objects
        //
        //  MATLAB Application
        public MatlabApp MyMatlabApp { get; set; }

        //
        //  Active Model being built
        public SimulinkModel? ActiveModel { get; set; }

        //
        //  Build Log (tracks executed MATLAB commands for diagnostics)
        public List<string> CommandLog { get; set; } = new();
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region

        //
        //  Model Lifecycle
        //
        //  Create a new Simulink model and open its diagram
        public SimulinkModel? CreateModel(string modelName)
        {
            string cmdNew = $"new_system('{modelName}')";
            string cmdOpen = $"open_system('{modelName}')";

            if (!ExecuteCommand(cmdNew) || !ExecuteCommand(cmdOpen))
            {
                return null;
            }

            SimulinkModel model = new()
            {
                MyMatlabApp = MyMatlabApp,
                SolverName = "ode45"
            };

            ActiveModel = model;
            return model;
        }

        //
        //  Open an existing Simulink model from disk
        public SimulinkModel? OpenModel(string modelPath)
        {
            string cmdOpen = $"open_system('{EscapePath(modelPath)}')";

            if (!ExecuteCommand(cmdOpen))
            {
                return null;
            }

            SimulinkModel model = new()
            {
                MyMatlabApp = MyMatlabApp
            };

            ActiveModel = model;
            return model;
        }

        //
        //  Save the current model
        public bool SaveModel(SimulinkModel model, string modelName)
        {
            return ExecuteCommand($"save_system('{modelName}')");
        }

        //
        //  Save the model to a specific path
        public bool SaveModelAs(SimulinkModel model, string modelName, string filePath)
        {
            return ExecuteCommand($"save_system('{modelName}', '{EscapePath(filePath)}')");
        }

        //
        //  Close the model
        public bool CloseModel(string modelName, bool saveFirst = false)
        {
            if (saveFirst)
            {
                SaveModel(ActiveModel!, modelName);
            }

            bool result = ExecuteCommand($"close_system('{modelName}', 0)");
            if (result && ActiveModel is not null)
            {
                ActiveModel = null;
            }
            return result;
        }

        //
        //  Block Management
        //
        //  Add a block from a Simulink library to the model
        //  libraryPath: e.g. "simulink/Sources/Constant"
        //  blockName:   the desired name in the model
        public SimulinkBlock? AddBlock(SimulinkModel model, string modelName,
            string libraryPath, string blockName)
        {
            string destPath = $"{modelName}/{blockName}";
            string cmd = $"add_block('{libraryPath}', '{destPath}')";

            if (!ExecuteCommand(cmd))
            {
                return null;
            }

            SimulinkBlock block = new()
            {
                Name = blockName,
                Path = destPath,
                //LibraryPath = libraryPath,
                CurrentSimulinkModel = model
            };

            model.MyBlocks.Add(block);
            model.CurrentBlock = block;
            return block;
        }

        //
        //  Remove a block from the model
        public bool RemoveBlock(SimulinkModel model, string modelName, SimulinkBlock block)
        {
            string blockPath = $"{modelName}/{block.Name}";
            string cmd = $"delete_block('{blockPath}')";

            if (!ExecuteCommand(cmd))
            {
                return false;
            }

            //  Disconnect any signals associated with this block
            model.MySignals.RemoveAll(s =>
                s.SourceBlock == block || s.DestinationBlock == block);

            model.MyBlocks.Remove(block);
            block.CurrentSimulinkModel = null;
            return true;
        }

        //
        //  Set a parameter on a block in the live Simulink model
        public bool SetBlockParameter(string modelName, SimulinkBlock block,
            string parameterName, string parameterValue)
        {
            string blockPath = $"{modelName}/{block.Name}";
            string cmd = $"set_param('{blockPath}', '{parameterName}', '{parameterValue}')";

            if (!ExecuteCommand(cmd))
            {
                return false;
            }

            //  Keep the domain model in sync
            SimulinkBlockParameter? existing = block.FindBlockParameter(parameterName);
            if (existing is not null)
            {
                existing.SetValue(parameterValue);
            }
            else
            {
                block.AddBlockParameter(new SimulinkBlockParameter(parameterName, parameterValue));
            }
            return true;
        }

        //
        //  Set the position of a block  [left top right bottom]
        public bool SetBlockPosition(string modelName, SimulinkBlock block,
            int left, int top, int right, int bottom)
        {
            string blockPath = $"{modelName}/{block.Name}";
            string posArray = $"[{left} {top} {right} {bottom}]";
            string cmd = $"set_param('{blockPath}', 'Position', '{posArray}')";

            return ExecuteCommand(cmd);
        }

        //
        //  Signal / Line Management
        //
        //  Connect an output port of one block to an input port of another
        //  Port numbers are 1-based (Simulink convention)
        public SimulinkSignal? ConnectBlocks(SimulinkModel model, string modelName,
            SimulinkBlock sourceBlock, int sourcePortNumber,
            SimulinkBlock destBlock, int destPortNumber)
        {
            string srcPort = $"{sourceBlock.Name}/{sourcePortNumber}";
            string dstPort = $"{destBlock.Name}/{destPortNumber}";
            string cmd = $"add_line('{modelName}', '{srcPort}', '{dstPort}', 'autorouting', 'smart')";

            if (!ExecuteCommand(cmd))
            {
                return null;
            }

            //  Create domain signal object
            SimulinkSignal signal = new()
            {
                Name = $"{sourceBlock.Name}_{sourcePortNumber}_to_{destBlock.Name}_{destPortNumber}",
                CurrentSimulinkModel = model
            };
            signal.SetSource(sourceBlock, sourcePortNumber);
            signal.SetDestination(destBlock, destPortNumber);

            //  Update block connectivity
            sourceBlock.ConnectOutput(signal);
            destBlock.ConnectInput(signal);

            //  Connect port objects if they exist
            SimulinkBlockPort? srcPortObj = sourceBlock.FindPort(
                SimulinkBlockPort.PortDirection.Output, sourcePortNumber);
            SimulinkBlockPort? dstPortObj = destBlock.FindPort(
                SimulinkBlockPort.PortDirection.Input, destPortNumber);
            srcPortObj?.Connect(signal);
            dstPortObj?.Connect(signal);

            //  Register signal with the model
            model.MySignals.Add(signal);
            model.CurrentSignal = signal;
            return signal;
        }

        //
        //  Disconnect a signal line between two blocks
        public bool DisconnectBlocks(SimulinkModel model, string modelName,
            SimulinkBlock sourceBlock, int sourcePortNumber,
            SimulinkBlock destBlock, int destPortNumber)
        {
            string srcPort = $"{sourceBlock.Name}/{sourcePortNumber}";
            string dstPort = $"{destBlock.Name}/{destPortNumber}";
            string cmd = $"delete_line('{modelName}', '{srcPort}', '{dstPort}')";

            if (!ExecuteCommand(cmd))
            {
                return false;
            }

            //  Remove matching signal from domain model
            SimulinkSignal? signal = model.MySignals.Find(s =>
                s.SourceBlock == sourceBlock &&
                s.SourcePortIndex == sourcePortNumber &&
                s.DestinationBlock == destBlock &&
                s.DestinationPortIndex == destPortNumber);

            if (signal is not null)
            {
                //  Disconnect port objects
                SimulinkBlockPort? srcPortObj = sourceBlock.FindPort(
                    SimulinkBlockPort.PortDirection.Output, sourcePortNumber);
                SimulinkBlockPort? dstPortObj = destBlock.FindPort(
                    SimulinkBlockPort.PortDirection.Input, destPortNumber);
                srcPortObj?.Disconnect();
                dstPortObj?.Disconnect();

                model.MySignals.Remove(signal);
            }
            return true;
        }

        //
        //  Model Configuration
        //
        //  Set the solver for the model
        public bool SetSolver(SimulinkModel model, string modelName,
            string solverName, SolverType solverType)
        {
            string typeStr = solverType == SolverType.FixedStep ? "FixedStepDiscrete" : "VariableStepAuto";
            bool typeResult = ExecuteCommand(
                $"set_param('{modelName}', 'SolverType', '{typeStr}')");
            bool solverResult = ExecuteCommand(
                $"set_param('{modelName}', 'Solver', '{solverName}')");

            if (typeResult && solverResult)
            {
                model.SolverName = solverName;
            }
            return typeResult && solverResult;
        }

        //
        //  Set the simulation stop time
        public bool SetStopTime(string modelName, double stopTime)
        {
            string timeStr = stopTime.ToString(CultureInfo.InvariantCulture);
            return ExecuteCommand($"set_param('{modelName}', 'StopTime', '{timeStr}')");
        }

        //
        //  Set the fixed step size (for fixed-step solvers)
        public bool SetFixedStepSize(string modelName, double stepSize)
        {
            string sizeStr = stepSize.ToString(CultureInfo.InvariantCulture);
            return ExecuteCommand($"set_param('{modelName}', 'FixedStep', '{sizeStr}')");
        }

        //
        //  Simulation
        //
        //  Run a simulation on the model
        public bool RunSimulation(string modelName)
        {
            return ExecuteCommand($"sim('{modelName}')");
        }

        //
        //  Internal Helpers
        //
        //  Execute a MATLAB command through the MatlabApp and log it
        private bool ExecuteCommand(string command)
        {
            CommandLog.Add(command);
            return MyMatlabApp.RunMatlabCommand(command);
        }

        //
        //  Escape backslashes in file paths for MATLAB strings
        private static string EscapePath(string path)
        {
            return path.Replace("\\", "/");
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  EVENTS
        //
        //  ************************************************************
        #region

        #endregion
        //  *****************************************************************************************
    }
}
