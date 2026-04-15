using System.Windows;
using System.Collections.Generic;
using Applications;
using MLApp;
using SE_Library;

namespace MatlabLib
{
    public class MatlabApp : ApplicationClass
    {

        //  *****************************************************************************************
        // DECLARATIONS
        //
        //  Projects (No properties exposed for these yet, keeping as fields)
        private MatlabProject _CurrentProject;
        private List<MatlabProject> _MyProjects;

        //  *****************************************************************************************

        //  *****************************************************************************************
        //  MATLABAPP CONSTRUCTOR
        //
        public MatlabApp(ApplicationManager myAppMgr)
        {
            this.MyApplicationManager = myAppMgr;
            this.TheMatlabAPI = new MatlabAPI();
            this._MyProjects = new List<MatlabProject>();
            this.Toolboxes = new List<ApplicationAddOn>();
            this.Blocksets = new List<ApplicationAddOn>();
            this._CurrentProject = new MatlabProject();
        }
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  PROPERTIES
        //
        //  **************
        //  Owned & Owning Objects
        //
        //  The Application Manager
        public ApplicationManager MyApplicationManager { get; set; }
        
        //
        //  The Matlab Application objects
        //
        //  Existing Application
        public MLApp.MLApp? ExistingMatlabApp { get; set; }
        
        //
        //  Current Application
        public MLApp.MLApp? CurrentMatlabApp { get; set; }
        
        //
        //  The Matlab API
        public MatlabAPI TheMatlabAPI { get; set; }      
        
        //  **************
        //  Add-Ons
        //
        //  Toolboxes
        public List<ApplicationAddOn> Toolboxes { get; set; }
        
        //
        //  Blocksets
        public List<ApplicationAddOn> Blocksets { get; set; }

        //  **************
        //  Booleans
        //
        //  Matlab is Running
        public Boolean MatlabRunning { get; set; }
        
        //
        //  Has Simulink
        public Boolean HasSimulink { get; set; }
        
        //
        //  Has SimScape
        public Boolean HasSimScape { get; set; }


        //  *****************************************************************************************

        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
       //  CREATE A MATLAB APPLICATION AND MAKE IT CURRENT
        #region
        public bool CreateMatlabApp()
        {
            try
            {
                this.CurrentMatlabApp = new MLApp.MLApp();
                this.CurrentMatlabApp.Execute("desktop");
                this.CurrentMatlabApp.Visible = 1;
                
                return true;

            }

            catch
            {
                return false;
            }
        }
        //  
        //  Run a Command
        public Boolean RunMatlabCommand(String cmd)
        {
            try
            {
                if (this.CurrentMatlabApp == null)
                {
                    Console.WriteLine("CurrentMatlabApp is not initialized.");
                    return false;
                }
                this.CurrentMatlabApp.Execute(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't Run Matlab Command: {ex.Message}");
                return false;
            }
        }
        //
        //  Start Simulink
        public Boolean StartSimulink()
        {
            try
            {
                RunMatlabCommand("Simulink");
                this.MatlabRunning = true;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't Start Simulink: {ex.Message}");
                return false;
            }
        }
        #endregion
        //  *****************************************************************************************

        // ... (Events regions remain unchanged)
    }
}
