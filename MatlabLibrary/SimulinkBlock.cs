using System;
using System.Collections.Generic;
using Propulsion;
using Power;
using Structure;
using SensorNamespace;
//using ThermalManagement;
//using GNC;
//using Communications;
//using Fluidics;
using Data;
using CAD;
using Controls;
using SystemsEngineering;

using SystemsEngineering;

namespace Simulation
{
    public class SimulinkBlock :SimulationModelElement
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
        private SimulinkBlockLibrary _MyBlockLibrary;
        //
        //  Owned & Owning Objects
        private SimulinkModel _CurrentSimulinkModel;
        private List<SimulinkModel> _MySimulinkModels;
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
        public SimulinkBlock(SimulationModel myModel) : base(myModel)
        {
            //
            //  Identification
            this.MySimType = SimElementTypeEnum.SimulinkBlock;
            //  Lists
            this.MySimulinkModels = new List<SimulinkModel>();
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
        public SimulinkBlockLibrary MyBlockLibrary
        {
            set => _MyBlockLibrary = value;
            get { return _MyBlockLibrary; }
        }
        //
        //  Owned & Owning Objects
        public SimulinkModel CurrentSimulinkModel
        {
            set => _CurrentSimulinkModel = value;
            get { return _CurrentSimulinkModel; }
        }
        public List<SimulinkModel> MySimulinkModels
        {
            set => _MySimulinkModels = value;
            get { return _MySimulinkModels; }
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
