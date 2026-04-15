using System.Windows;
using System.Collections.Generic;
using Applications;
using MLApp;
using SE_Library;

namespace MatlabLib
{
    public class MatlabProject
    {
        //  *****************************************************************************************
        //  DECLARATIONS & INITIALIZATIONS
        //
        //  ************************************************************
        #region
        // All fields were removed because we are using auto-properties.
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  ENUMERATIONS
        //
        //  ************************************************************
        #region
        public enum ProjectStateEnum
        {
            Closed = 0,
            Open,
            Loading,
            Saving
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  MATLABPROJECT CONSTRUCTOR
        //
        //  ************************************************************
        #region
        public MatlabProject()
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
        //  Name
        public String Name { get; set; } = string.Empty;

        //
        //  Version
        public String Version { get; set; } = string.Empty;

        //
        //  Description
        public String Description { get; set; } = string.Empty;

        //
        //  Author
        public String Author { get; set; } = string.Empty;

        //
        //  Project Path (root folder of the .prj / .mlproj)
        public String ProjectPath { get; set; } = string.Empty;

        //
        //  Data
        //
        //  Project State
        public ProjectStateEnum State { get; set; } = ProjectStateEnum.Closed;

        //
        //  Has Unsaved Changes
        public bool IsDirty { get; set; } = false;

        //
        //  Timestamps
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

        //
        //  Startup & Shutdown Scripts
        public String StartupScript { get; set; } = string.Empty;
        public String ShutdownScript { get; set; } = string.Empty;

        //
        //  Owned & Owning Objects
        //
        //  Owning Matlab Application
        public MatlabApp? MyMatlabApp { get; set; }

        //
        //  Matlab Files
        public MatlabFile? CurrentFile { get; set; }
        public List<MatlabFile> MyFiles { get; set; } = new();

        //
        //  Simulink Models
        public SimulinkModel? CurrentSimulinkModel { get; set; }
        public List<SimulinkModel> MySimulinkModels { get; set; } = new();

        //
        //  Required Toolboxes
        public List<MatlabToolbox> RequiredToolboxes { get; set; } = new();

        //
        //  Required Blocksets
        public List<SimulinkBlockset> RequiredBlocksets { get; set; } = new();

        //
        //  Project Paths (folders on the MATLAB path for this project)
        public List<string> ProjectPaths { get; set; } = new();

        //
        //  Environment Variables
        public Dictionary<string, string> EnvironmentVariables { get; set; } = new();
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region
        //
        //  Project Lifecycle
        public bool Open(string projectPath)
        {
            ProjectPath = projectPath;
            State = ProjectStateEnum.Loading;
            // TODO: Load project metadata from .prj / .mlproj file
            State = ProjectStateEnum.Open;
            IsDirty = false;
            return true;
        }

        public bool Close()
        {
            if (IsDirty)
            {
                return false; // Caller should save first
            }

            State = ProjectStateEnum.Closed;
            return true;
        }

        public bool Save()
        {
            State = ProjectStateEnum.Saving;
            LastModifiedDate = DateTime.Now;
            // TODO: Persist project metadata to disk
            IsDirty = false;
            State = ProjectStateEnum.Open;
            return true;
        }

        //
        //  File Management
        public void AddFile(MatlabFile file)
        {
            if (!MyFiles.Contains(file))
            {
                MyFiles.Add(file);
                IsDirty = true;
            }
        }

        public bool RemoveFile(MatlabFile file)
        {
            bool removed = MyFiles.Remove(file);
            if (removed)
            {
                IsDirty = true;
            }
            return removed;
        }

        public MatlabFile? FindFileByName(string fileName)
        {
            return MyFiles.Find(f => f.Name == fileName);
        }

        //
        //  Model Management
        public void AddModel(SimulinkModel model)
        {
            if (!MySimulinkModels.Contains(model))
            {
                MySimulinkModels.Add(model);
                IsDirty = true;
            }
        }

        public bool RemoveModel(SimulinkModel model)
        {
            bool removed = MySimulinkModels.Remove(model);
            if (removed)
            {
                IsDirty = true;
            }
            return removed;
        }

        public SimulinkModel? FindModelByName(string modelName)
        {
            return MySimulinkModels.Find(m => m.MySystem.Name == modelName);
        }

        //
        //  Path Management
        public void AddPath(string path)
        {
            if (!ProjectPaths.Contains(path))
            {
                ProjectPaths.Add(path);
                IsDirty = true;
            }
        }

        public bool RemovePath(string path)
        {
            bool removed = ProjectPaths.Remove(path);
            if (removed)
            {
                IsDirty = true;
            }
            return removed;
        }

        //
        //  Dependency Management
        public void AddRequiredToolbox(MatlabToolbox toolbox)
        {
            if (!RequiredToolboxes.Contains(toolbox))
            {
                RequiredToolboxes.Add(toolbox);
                IsDirty = true;
            }
        }

        public bool RemoveRequiredToolbox(MatlabToolbox toolbox)
        {
            bool removed = RequiredToolboxes.Remove(toolbox);
            if (removed)
            {
                IsDirty = true;
            }
            return removed;
        }

        public void AddRequiredBlockset(SimulinkBlockset blockset)
        {
            if (!RequiredBlocksets.Contains(blockset))
            {
                RequiredBlocksets.Add(blockset);
                IsDirty = true;
            }
        }

        public bool RemoveRequiredBlockset(SimulinkBlockset blockset)
        {
            bool removed = RequiredBlocksets.Remove(blockset);
            if (removed)
            {
                IsDirty = true;
            }
            return removed;
        }

        //
        //  Environment Variables
        public void SetEnvironmentVariable(string variableName, string value)
        {
            EnvironmentVariables[variableName] = value;
            IsDirty = true;
        }

        public string? GetEnvironmentVariable(string variableName)
        {
            return EnvironmentVariables.TryGetValue(variableName, out string? value) ? value : null;
        }

        //
        //  Validation
        public bool AreAllDependenciesAvailable()
        {
            bool toolboxesAvailable = RequiredToolboxes.TrueForAll(t => t.IsAvailable());
            bool blocksetsAvailable = RequiredBlocksets.TrueForAll(b => b.IsAvailable());
            return toolboxesAvailable && blocksetsAvailable;
        }

        public bool IsOpen()
        {
            return State == ProjectStateEnum.Open;
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
