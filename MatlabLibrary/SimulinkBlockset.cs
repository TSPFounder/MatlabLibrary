using System.Windows;
using System.Collections.Generic;
using Applications;
using SE_Library;

namespace MatlabLib
{
    public class SimulinkBlockset : ApplicationAddOn
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
        public enum SimulinkBlocksetEnum
        {
            AerospaceBlockset = 0,
            RF_Blockset,
            Simulink3DAnimation,
            SimulinkControlDesign,
            SimulinkDesignOptimization,
            SimulinkReportGenerator
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  SIMULINKBLOCKSET CONSTRUCTOR
        //
        //  ************************************************************
        #region
        public SimulinkBlockset()
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
        public string LibraryPath { get; set; } = string.Empty;

        //
        //  Data
        public SimulinkBlocksetEnum MyBlocksetEnum { get; set; }
        public new bool IsInstalled { get; set; } = false;
        public bool IsLicensed { get; set; } = false;

        //
        //  Owned & Owning Objects
        //
        //  Simulink Blocks
        public SimulinkBlock? CurrentBlock { get; set; }
        public List<SimulinkBlock> MyBlocks { get; set; } = new();

        //
        //  Simulink Models
        public SimulinkModel? CurrentSimulinkModel { get; set; }
        public List<SimulinkModel> MySimulinkModels { get; set; } = new();
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

        public List<SimulinkBlock> GetBlocksByType(string blockType)
        {
            return MyBlocks.FindAll(b => b.BlockType == blockType);
        }

        //
        //  Validation
        public bool ContainsBlock(string blockName)
        {
            return MyBlocks.Exists(b => b.Name == blockName);
        }

        public bool IsAvailable()
        {
            return IsInstalled && IsLicensed;
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
