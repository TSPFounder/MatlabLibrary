using System.Windows;
using System.Collections.Generic;
using Applications;
using MLApp;
using SE_Library;
using Mathematics;

namespace MatlabLib
{
    public class SimulinkBlock 
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


        //  ****************************************************************************************
        //  INITIALIZATIONS
        //
        //  ************************************************************
        #region

        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  ENUMERATIONS
        //
        //  ************************************************************
        #region
        //
        //  Simulink Block Library
        public enum SimulinkBlockLibrary
        {
            Simulink = 0,
            AerospaceBlockset,
            AudioToolbox,
            CommunicationsToolbox,
            CommunicationsToolboxHDL_Support,
            ComputerVisionToolbox,
            ControlSystemToolbox,
            DeepLearningToolbox,
            DSP_SystemToolbox,
            DSP_SystemToolboxHDL_Support,
            FixedPointDesigner,
            FixedPointDesignerHDL_Support,
            HDL_Coder,
            ImageAcquisitionToolbox,
            ModelPredictiveControlToolbox,
            NavigationToolbox,
            PhasedArraySystemsToolbox,
            ReinforcementLearning,
            ReportGenerator,
            RF_Blockset,
            RoboticsSystemToolbox,
            ROS_Toolbox,
            Simulink3DAnimation,
            SimulinkCoder,
            SimulinkControlDesign,
            SimulinkDesignOptimization,
            SimulinkExtras,
            ArduinoSupportPackage,
            ParrotMinidronePackage,
            RaspberryPiSupportPackage,
            Stateflow,
            StatisticsAndMachineLearningToolbox,
            UAV_Toolbox,
            UAV_ToolboxForPX4
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  SIMULINKBLOCK CONSTRUCTOR
        //
        //  ************************************************************
        #region
        public SimulinkBlock()
        {
            //
            //  Identification
            
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  PROPERTIES
        //
        //  ************************************************************
        #region
        //  
        //  Identification
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string BlockType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //
        //  Data
        public SimulinkBlockLibrary MyBlockLibrary { get; set; }            

        //  UI / Layout (Represents Simulink's [left top right bottom] Position)
        public Quadrilateral Position { get; set; } = new Quadrilateral();

        //
        //  Owned & Owning Objects
        public SimulinkModel? CurrentSimulinkModel { get; set; }
        public List<SimulinkModel> MySimulinkModels { get; set; } = new();

        //
        //  Ports / Signals
        public List<SimulinkSignal> InputSignals { get; set; } = new();
        public List<SimulinkSignal> OutputSignals { get; set; } = new();

        //
        //  Block Ports
        public SimulinkBlockPort? CurrentBlockPort { get; set; }
        public List<SimulinkBlockPort> InputPorts { get; set; } = new();
        public List<SimulinkBlockPort> OutputPorts { get; set; } = new();

        //
        //  Block Parameters (typed parameter objects)
        public SimulinkBlockParameter? CurrentBlockParameter { get; set; }
        public List<SimulinkBlockParameter> BlockParameters { get; set; } = new();
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region
        

        //
        //  Block Port Management
        public void AddPort(SimulinkBlockPort port)
        {
            List<SimulinkBlockPort> targetList = port.IsInput() ? InputPorts : OutputPorts;
            if (!targetList.Exists(p => p.PortNumber == port.PortNumber && p.Direction == port.Direction))
            {
                port.OwningBlock = this;
                targetList.Add(port);
            }
        }

        public bool RemovePort(SimulinkBlockPort port)
        {
            List<SimulinkBlockPort> targetList = port.IsInput() ? InputPorts : OutputPorts;
            if (targetList.Remove(port))
            {
                port.OwningBlock = null;
                port.Disconnect();
                return true;
            }
            return false;
        }

        public SimulinkBlockPort? FindPortByName(string portName)
        {
            return InputPorts.Find(p => p.Name == portName)
                ?? OutputPorts.Find(p => p.Name == portName);
        }

        public SimulinkBlockPort? FindPort(SimulinkBlockPort.PortDirection direction, int portNumber)
        {
            List<SimulinkBlockPort> targetList = direction == SimulinkBlockPort.PortDirection.Input
                ? InputPorts
                : OutputPorts;
            return targetList.Find(p => p.PortNumber == portNumber);
        }

        public List<SimulinkBlockPort> GetAllPorts()
        {
            List<SimulinkBlockPort> allPorts = new(InputPorts.Count + OutputPorts.Count);
            allPorts.AddRange(InputPorts);
            allPorts.AddRange(OutputPorts);
            return allPorts;
        }

        public List<SimulinkBlockPort> GetConnectedPorts()
        {
            return GetAllPorts().FindAll(p => p.IsConnected());
        }

        public List<SimulinkBlockPort> GetUnconnectedPorts()
        {
            return GetAllPorts().FindAll(p => !p.IsConnected());
        }

        //
        //  Block Parameter Management
        public void AddBlockParameter(SimulinkBlockParameter parameter)
        {
            if (!BlockParameters.Exists(p => p.Name == parameter.Name))
            {
                parameter.OwningBlock = this;
                BlockParameters.Add(parameter);
            }
        }

        public bool RemoveBlockParameter(string parameterName)
        {
            SimulinkBlockParameter? param = FindBlockParameter(parameterName);
            if (param is not null)
            {
                param.OwningBlock = null;
                return BlockParameters.Remove(param);
            }
            return false;
        }

        public SimulinkBlockParameter? FindBlockParameter(string parameterName)
        {
            return BlockParameters.Find(p => p.Name == parameterName);
        }

        public void SetBlockParameterValue(string parameterName, string value)
        {
            SimulinkBlockParameter? param = FindBlockParameter(parameterName);
            if (param is not null)
            {
                param.SetValue(value);
            }
            else
            {
                //  Create a new parameter if it doesn't exist
                var newParam = new SimulinkBlockParameter(parameterName, value)
                {
                    OwningBlock = this
                };
                BlockParameters.Add(newParam);
            }
        }

        public string? GetBlockParameterValue(string parameterName)
        {
            SimulinkBlockParameter? param = FindBlockParameter(parameterName);
            return param?.Value;
        }

        public bool ValidateAllParameters()
        {
            return BlockParameters.TrueForAll(p => p.Validate());
        }

        public List<SimulinkBlockParameter> GetTunableParameters()
        {
            return BlockParameters.FindAll(p => p.IsTunable);
        }

        public List<SimulinkBlockParameter> GetModifiedParameters()
        {
            return BlockParameters.FindAll(p => p.HasCustomValue());
        }

        //
        //  Connectivity
        public void ConnectInput(SimulinkSignal signal)
        {
            if (!InputSignals.Contains(signal))
            {
                InputSignals.Add(signal);
            }
        }

        public void ConnectOutput(SimulinkSignal signal)
        {
            if (!OutputSignals.Contains(signal))
            {
                OutputSignals.Add(signal);
            }
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
