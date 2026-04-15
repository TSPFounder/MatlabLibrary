using System.Windows;
using System.Collections.Generic;
using Applications;
using MLApp;
using SE_Library;

namespace MatlabLib
{
    public class MatlabFile : AppFile
    {
        //  *****************************************************************************************
        //  DECLARATIONS
        //
        //  ************************************************************
        //
        //  Identification

        //  *****************************************************************************************


        //  *****************************************************************************************
        //  INITIALIZATIONS
        //
        //  ************************************************************

        //  *****************************************************************************************


        //  *****************************************************************************************
        //  ENUMERATIONS
        //
        //  ************************************************************
        #region
        public enum MatlabFileTypeEnum
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

        public enum MatlabFileStateEnum
        {
            Closed = 0,
            Open,
            Modified,
            ReadOnly
        }

        public enum MatlabEncodingEnum
        {
            UTF8 = 0,
            ASCII,
            Latin1,
            ShiftJIS,
            SystemDefault
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  MATLABFILE CONSTRUCTOR
        //
        //  ************************************************************
        public MatlabFile()
        {

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
        //  Name
        public new string Name { get; set; } = string.Empty;

        //
        //  File Path (full path on disk)
        public string FilePath { get; set; } = string.Empty;

        //
        //  Description
        public string Description { get; set; } = string.Empty;

        //
        //  Author
        public string Author { get; set; } = string.Empty;

        //
        //  Matlab File Type
        public MatlabFileTypeEnum MatlabFileType { get; set; }

        //
        //  Data
        //
        //  File State
        public MatlabFileStateEnum State { get; set; } = MatlabFileStateEnum.Closed;

        //
        //  Encoding
        public MatlabEncodingEnum Encoding { get; set; } = MatlabEncodingEnum.UTF8;

        //
        //  File Size (in bytes)
        public long FileSize { get; set; } = 0;

        //
        //  Timestamps
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

        //
        //  Content (source code for .m / .mlx files)
        public string Content { get; set; } = string.Empty;

        //
        //  Is Read-Only
        public bool IsReadOnly { get; set; } = false;

        //
        //  Has Unsaved Changes
        public bool IsDirty { get; set; } = false;

        //
        //  Functions defined in this file
        public List<string> DefinedFunctions { get; set; } = new();

        //
        //  Variables (workspace variables for .mat files)
        public Dictionary<string, string> Variables { get; set; } = new();

        //
        //  Dependencies (other MATLAB files this file depends on)
        public List<MatlabFile> Dependencies { get; set; } = new();

        //
        //  Required Toolboxes
        public List<MatlabToolbox> RequiredToolboxes { get; set; } = new();

        //
        //  Owned & Owning Objects
        //
        //  Owning Matlab Project
        public MatlabProject? MyProject { get; set; }

        //
        //  Owning Matlab Application
        public MatlabApp? MyMatlabApp { get; set; }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region
        //
        //  File Lifecycle
        public bool Open(string filePath)
        {
            FilePath = filePath;
            State = MatlabFileStateEnum.Open;
            IsDirty = false;
            return true;
        }

        public bool Close()
        {
            if (IsDirty)
            {
                return false; // Caller should save first
            }

            State = MatlabFileStateEnum.Closed;
            Content = string.Empty;
            return true;
        }

        public bool Save()
        {
            if (IsReadOnly)
            {
                return false;
            }

            LastModifiedDate = DateTime.Now;
            IsDirty = false;
            State = MatlabFileStateEnum.Open;
            return true;
        }

        //
        //  Content Management
        public void UpdateContent(string newContent)
        {
            if (!IsReadOnly)
            {
                Content = newContent;
                IsDirty = true;
                State = MatlabFileStateEnum.Modified;
            }
        }

        //
        //  Function Management
        public void AddDefinedFunction(string functionName)
        {
            if (!DefinedFunctions.Contains(functionName))
            {
                DefinedFunctions.Add(functionName);
            }
        }

        public bool RemoveDefinedFunction(string functionName)
        {
            return DefinedFunctions.Remove(functionName);
        }

        public bool ContainsFunction(string functionName)
        {
            return DefinedFunctions.Contains(functionName);
        }

        //
        //  Variable Management (for .mat files)
        public void SetVariable(string variableName, string value)
        {
            Variables[variableName] = value;
            IsDirty = true;
        }

        public string? GetVariable(string variableName)
        {
            return Variables.TryGetValue(variableName, out string? value) ? value : null;
        }

        public bool RemoveVariable(string variableName)
        {
            bool removed = Variables.Remove(variableName);
            if (removed)
            {
                IsDirty = true;
            }
            return removed;
        }

        //
        //  Dependency Management
        public void AddDependency(MatlabFile file)
        {
            if (!Dependencies.Contains(file))
            {
                Dependencies.Add(file);
            }
        }

        public bool RemoveDependency(MatlabFile file)
        {
            return Dependencies.Remove(file);
        }

        public MatlabFile? FindDependencyByName(string fileName)
        {
            return Dependencies.Find(f => f.Name == fileName);
        }

        //
        //  Required Toolbox Management
        public void AddRequiredToolbox(MatlabToolbox toolbox)
        {
            if (!RequiredToolboxes.Contains(toolbox))
            {
                RequiredToolboxes.Add(toolbox);
            }
        }

        public bool RemoveRequiredToolbox(MatlabToolbox toolbox)
        {
            return RequiredToolboxes.Remove(toolbox);
        }

        //
        //  Execution via MATLAB
        public bool Run(MatlabApp matlabApp)
        {
            if (MatlabFileType != MatlabFileTypeEnum.M && MatlabFileType != MatlabFileTypeEnum.MLX)
            {
                return false; // Only script/function files can be run
            }

            try
            {
                string command = System.IO.Path.GetFileNameWithoutExtension(Name);
                return matlabApp.RunMatlabCommand(command);
            }
            catch
            {
                return false;
            }
        }

        //
        //  Validation
        public bool IsOpen()
        {
            return State != MatlabFileStateEnum.Closed;
        }

        public bool IsScript()
        {
            return MatlabFileType == MatlabFileTypeEnum.M || MatlabFileType == MatlabFileTypeEnum.MLX;
        }

        public bool IsDataFile()
        {
            return MatlabFileType == MatlabFileTypeEnum.MAT;
        }

        public string GetFileExtension()
        {
            return MatlabFileType switch
            {
                MatlabFileTypeEnum.MAT => ".mat",
                MatlabFileTypeEnum.MX => ".mx",
                MatlabFileTypeEnum.M => ".m",
                MatlabFileTypeEnum.MEX => ".mex",
                MatlabFileTypeEnum.MLIB => ".mlib",
                MatlabFileTypeEnum.MLAPP => ".mlapp",
                MatlabFileTypeEnum.MLPROJ => ".mlproj",
                MatlabFileTypeEnum.MLTBX => ".mltbx",
                MatlabFileTypeEnum.MLX => ".mlx",
                _ => string.Empty
            };
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
