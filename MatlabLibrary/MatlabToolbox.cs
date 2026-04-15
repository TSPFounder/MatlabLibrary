using System.Windows;
using System.Collections.Generic;
using Applications;
using MLApp;
using SE_Library;

namespace MatlabLib
{
    public class MatlabToolbox :ApplicationAddOn
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
        public enum MatlabToolboxEnum
        {
            AerospaceToolbox=0,
            AntennaToolbox,
            AudioToolbox,
            CommunicationsToolbox,
            ComputerVisionToolbox,
            ControlSystemToolbox,
            DataAcquisitionToolbox,
            DeepLearningToolbox,
            DSP_SystemToolbox,
            ImageAcquisitionToolbox,
            ImageAcquisitionToolboxSupportPackageForKinect,
            ImageProcessingToolbox,
            MappingToolbox,
            MatlabReportGenerator,
            NavigationToolbox,
            OptimizationToolbox,
            ParallelComputingToolbox,
            PartialDifferentialEquationToolbox,
            PhasedArraySystemToolbox,
            ReinforcementLearningToolbox,
            RF_PCB_Toolbox,
            RF_Toolbox,
            RoboticsSystemToolbox,
            ROS_Toolbox,
            SignalProcessingToolbox,
            ArduinoSupportPackage,
            ParrotMinidronePackage,
            RaspberryPiSupportPackage,
            Stateflow,
            StatisticsAndMachineLearningToolbox,
            SymbolicMathToolbox,
            TestAnalyticsToolbox,
            UAV_Toolbox,
            UAV_ToolboxForPX4
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  MATLABTOOLBOX CONSTRUCTOR
        //
        //  ************************************************************
        #region
        public MatlabToolbox()
            {

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
        public new string Version { get; set; } = string.Empty;
        public new string Description { get; set; } = string.Empty;
        public string InstallPath { get; set; } = string.Empty;

        //
        //  Data
        public MatlabToolboxEnum MyToolboxEnum { get; set; }
        public new bool IsInstalled { get; set; } = false;
        public bool IsLicensed { get; set; } = false;

        //
        //  Owned & Owning Objects
        //
        //  Simulink Blocks
        public SimulinkBlock? CurrentBlock { get; set; }
        
        public List<SimulinkBlock> MyBlocks { get; set; } = new();

        //
        //  Functions provided by this toolbox
        public List<string> MyFunctions { get; set; } = new();

        //
        //  Owning Matlab Application
        public MatlabApp? CurrentMatlabApp { get; set; }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region
        //
        //  Block Management
        public void AddBlock(SimulinkBlock block)
        {
            if (!MyBlocks.Contains(block))
            {
                MyBlocks.Add(block);
            }
        }

        public bool RemoveBlock(SimulinkBlock block)
        {
            return MyBlocks.Remove(block);
        }

        public SimulinkBlock? FindBlockByName(string blockName)
        {
            return MyBlocks.Find(b => b.Name == blockName);
        }

        //
        //  Function Management
        public void AddFunction(string functionName)
        {
            if (!MyFunctions.Contains(functionName))
            {
                MyFunctions.Add(functionName);
            }
        }

        public bool RemoveFunction(string functionName)
        {
            return MyFunctions.Remove(functionName);
        }

        public bool ContainsFunction(string functionName)
        {
            return MyFunctions.Contains(functionName);
        }

        //
        //  Validation
        public bool IsAvailable()
        {
            return IsInstalled && IsLicensed;
        }

        //
        //  Toolbox Verification via MATLAB
        public bool VerifyInstallation(MatlabApp matlabApp)
        {
            try
            {
                //  Uses MATLAB's 'ver' command to check toolbox presence
                return matlabApp.RunMatlabCommand($"ver('{Name}')");
            }
            catch
            {
                return false;
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
