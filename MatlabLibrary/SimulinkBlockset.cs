using System.Windows;
using System.Windows.Forms;

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
            AerospaceBlockset=0,
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
