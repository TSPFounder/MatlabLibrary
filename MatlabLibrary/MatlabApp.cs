using System;
using System.Windows.Forms;
using System.Collections.Generic;
using CAD;
using MLApp;

namespace SystemsEngineering
{
    public class MatlabApp : ApplicationClass
    {

        //  *****************************************************************************************
        // DECLARATIONS
        //
        //  Identifications


        //  Owned & Owning Objects
        //
        //  The Application Manager
        private ApplicationManager _MyApplicationManager;
        //
        //  The Matlab App
        private MLApp.MLApp _ExistingMatlabApp;
        private MLApp.MLApp _CurrentMatlabApp;
        //
        //  Projects
        private MatlabProject _CurrentProject;
        private List<MatlabProject> _MyProjects;
        //
        //  The Matlab API
        private MatlabAPI _TheMatlabAPI;
        //
        //  Toolboxes
        private List<ApplicationAddOn> _Toolboxes;
        private List<ApplicationAddOn> _Blocksets;

        //  Booleans
        private Boolean _MatlabRunning;
        protected bool _HasSimulink;
        protected bool _HasSimScape;

        //  *****************************************************************************************


        //  *****************************************************************************************
        //  INITIALIZATIONS



        ///  *****************************************************************************************


        //  *****************************************************************************************
        //  MATLABAPP CONSTRUCTOR
        //
        public MatlabApp(ApplicationManager myAppMgr)
        {
            this.MyApplicationManager = myAppMgr;
            this.TheMatlabAPI = new MatlabAPI();
        }
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  PROPERTIES
        //
        //  **************
        //  Owned & Owning Objects
        //
        //  The Application Manager
        public ApplicationManager MyApplicationManager
        {
            set => _MyApplicationManager = value;
            get { return _MyApplicationManager; }
        }
        //
        //  The Matlab Application objects
        //
        //  Existing Application
        public MLApp.MLApp ExistingMatlabApp
        {
            set
            {
                _ExistingMatlabApp = value;
            }

            get
            {
                return _ExistingMatlabApp;
            }

        }
        //
        //  Current Application
        public MLApp.MLApp CurrentMatlabApp
        {
            set
            {
                _CurrentMatlabApp = value;
            }

            get
            {
                return _CurrentMatlabApp;
            }

        }
        //
        //  The Matlab API
        public MatlabAPI TheMatlabAPI      
        {
            set => _TheMatlabAPI = value;
            get { return _TheMatlabAPI; }
        }
        //  **************
        //  Add-Ons
        //
        //  Toolboxes
        public List<ApplicationAddOn> Toolboxes
        {
            set => _Toolboxes = value;
            get
            {
                return _Toolboxes;
            }
        }
        //
        //  Blocksets
        public List<ApplicationAddOn> Blocksets
        {
            set => _Blocksets = value;
            get
            {
                return _Blocksets;
            }
        }

        //  **************
        //  Booleans
        //
        //  Matlab is Running
        public Boolean MatlabRunning
        {

            set => _MatlabRunning = value;
            get
            {
                return _MatlabRunning;
            }
        }
        //
        //  Has Simulink
        public Boolean HasSimulink
        {

            set => _HasSimulink = value;
            get
            {
                return _HasSimulink;
            }
        }
        //
        //  Has SimScape
        public Boolean HasSimScape
        {

            set => _HasSimScape = value;
            get
            {
                return _HasSimScape;
            }
        }


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
                this.CurrentMatlabApp.Execute(cmd);
                return true;
            }
            catch
            {
                MessageBox.Show("Couldn't Run Matlab Command");
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
            catch
            {
                MessageBox.Show("Couldn't Start Simulink");
                return false;
            }
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  EVENTS
        //
        //  ************************************************************

        //  *****************************************************************************************
    }
}
