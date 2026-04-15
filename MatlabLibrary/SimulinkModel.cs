using System;
using System.Collections.Generic;
using Applications;
using MLApp;
using SE_Library;

namespace MatlabLib
{
    public class SimulinkModel 
    {
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
            // Empty constructor since we are using inline property initializers
        }
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  PROPERTIES
        //
        //  ************************************************************

        //
        //  Identification
        //
        public SimulinkFileTypeEnum SimulinkFileType { get; set; }
        public string SolverName { get; set; } = string.Empty;

        //
        //  Owned & Owning Objects
        //
        public MatlabApp MyMatlabApp { get; set; } = new(new ApplicationManager());

        //
        //  Stateflow Models
        //  Note: Made this nullable (?) to prevent a StackOverflowException from recursively 
        //  calling the SimulinkModel constructor infinitely.
        public SimulinkModel? CurrentStateflowModel { get; set; }
        public List<SimulinkModel> MyStateflowModels { get; set; } = new();

        //
        //  DWM System
        public SE_System MySystem { get; set; } = new();

        //
        //  Toolboxes & Blocksets
        public SimulinkBlockset CurrentBlockset { get; set; } = new();
        public List<SimulinkBlockset> MyBlocksets { get; set; } = new();

        //
        //  Blocks
        public SimulinkBlock CurrentBlock { get; set; } = new();
        public List<SimulinkBlock> MyBlocks { get; set; } = new();

        //
        //  Signals
        public SimulinkSignal CurrentSignal { get; set; } = new();
        public List<SimulinkSignal> MySignals { get; set; } = new();

        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        public bool OpenModel(string tempPath)
        {
            return true;
        }
        //  *****************************************************************************************
    }
}
