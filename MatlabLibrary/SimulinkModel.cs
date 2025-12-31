using SystemsEngineering;
using System;
using System.Collections.Generic;
using Propulsion;
using CAD;
using AircraftObjects;
using MissionsNamespace;
using Power;
using Structure;
using SensorNamespace;
//using ThermalManagement;
//using GNC;
//using Communications;
//using Fluidics;
using Data;
using Controls;
using MLApp;

namespace Simulation
{
    public class SimulinkModel : SimulationModel
    {
        //  *****************************************************************************************
        //  DECLARATIONS
        //
        //  ************************************************************
        #region
        //  
        //  Identification
        private SimulinkFileTypeEnum _SimulinkFileType;
        //
        //  Data
        private String _SolverName;
        //
        //  Owned & Owning Objects
        //
        //  Matlab App
        private MatlabApp _MyMatlabApp;
        //
        //  Simscape Models
        private SimscapeModel _CurrentSimscapeModel;
        private List<SimscapeModel> _MySimscapeModels;
        //
        //  Stateflow Models
        private SimulinkModel _CurrentStateflowModel;
        private List<SimulinkModel> _MyStateflowModels;
        //
        //  DWM System
        private DWM_System _MySystem;
        //
        //  Toolboxes & Blocksets
        private SimulinkBlockset _CurrentBlockset;
        private List<SimulinkBlockset> _MyBlocksets;
        //
        //  Blocks
        private SimulinkBlock _CurrentBlock;
        private List<SimulinkBlock> _MyBlocks;
        //
        //  Signals
        private SimulinkSignal _CurrentSignal;
        private List<SimulinkSignal> _MySignals;
        #endregion
        //  *****************************************************************************************


        //  ****************************************************************************************
        //  INITIALIZATIONS
        //
        //  ************************************************************

        //  *****************************************************************************************


        //  *****************************************************************************************
        //  ENUMERATIONS
        //
        //  ************************************************************
        public enum SimulinkFileTypeEnum
        {
            MAT = 0,
            MX,
            M,
            MEX,
            MLIB,
            MLAPP,
            MLPROJ,
            MLTBX,
            MLX
        }
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  SIMULINKMODEL CONSTRUCTOR
        //
        //  ************************************************************
        public SimulinkModel()
        {
            //
            //  My System
            this.MySystem = new DWM_System();
            this.MyModelType = SimModelType.Simulink;
        }
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  PROPERTIES
        //
        //  ************************************************************
        #region
        //
        //  Identification
        //
        //  Simulink File Type
        public SimulinkFileTypeEnum SimulinkFileType
        {
            set => _SimulinkFileType = value;
            get
            {
                return _SimulinkFileType;
            }
        }
        //
        //  Data
        public String SolverName
        {
            set => _SolverName = value;
            get { return _SolverName; }
        }
        //
        //  Owned & Owning Objects
        //
        //  Matlab File Type
        public MatlabApp MyMatlabApp
        {
            set => _MyMatlabApp = value;
            get
            {
                return _MyMatlabApp;
            }
        }
        //
        //  Simscape Models
        public SimscapeModel CurrentSimscapeModel
        {
            set => _CurrentSimscapeModel = value;
            get
            {
                return _CurrentSimscapeModel;
            }
        }
        public List<SimscapeModel> MySimscapeModels
        {
            set => _MySimscapeModels = value;
            get
            {
                return _MySimscapeModels;
            }
        }
        //
        //  Stateflow Models
        public SimulinkModel CurrentStateflowModel
        {
            set => _CurrentStateflowModel = value;
            get
            {
                return _CurrentStateflowModel;
            }
        }
        public List<SimulinkModel> MyStateflowModels
        {
            set => _MyStateflowModels = value;
            get
            {
                return _MyStateflowModels;
            }
        }
        //
        //  My DWM System
        public DWM_System MySystem
        {
            set => _MySystem = value;
            get { return _MySystem; }
        }
        //
        //  Toolboxes & Blocksets
        public SimulinkBlockset CurrentBlockset
        {
            set => _CurrentBlockset = value;
            get { return _CurrentBlockset; }
        }
        public List<SimulinkBlockset> MyBlocksets
        {
            set => _MyBlocksets = value;
            get { return _MyBlocksets; }
        }
        //
        //  Blocks
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
        //
        //  Signals
       public SimulinkSignal CurrentSignal
        {
            set => _CurrentSignal = value;
            get { return _CurrentSignal; }
        }
       public List<SimulinkSignal> MySignals
        {
            set => _MySignals = value;
            get { return _MySignals; }
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        public Boolean OpenModel(String tempPath)
        {
            return true;
        }
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  EVENTS
        //
        //  ************************************************************

        //  *****************************************************************************************
    }
}
