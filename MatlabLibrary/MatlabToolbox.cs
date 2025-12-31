using System.Windows;
using Microsoft.Win32;
//using OfficeApps;
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop;
using CAD;
using Mathematics;
//using UModelLib;
//using SldWorks;
using Simulation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Bson;
using MissionsNamespace;
using Project;

namespace SystemsEngineering
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
        private MatlabToolboxEnum _MyToolboxEnum;
        //
        //  Owned & Owning Objects
        private SimulinkBlock _CurrentBlock;
        private List<SimulinkBlock> _MyBlocks;
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

        //
        //  Data
        public MatlabToolboxEnum MyToolboxEnum
        {
            set => _MyToolboxEnum = value;
            get { return _MyToolboxEnum; }
        }
        //
        //  Owned & Owning Objects
        //
        //  Simulink Blocks
        public SimulinkBlock CurrentBlock
        {
            set => _CurrentBlock = value;
            get { return _CurrentBlock; }
        }
        public List<SimulinkBlock> MyBlocks
        {
            set => _MyBlocks = value;
            get { return _MyBlocks; }
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region

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
