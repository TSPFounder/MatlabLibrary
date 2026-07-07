using System;
using System.Collections.Generic;
using Applications;
using MLApp;
using SE_Library;

namespace MatlabLib
{
    public class MatlabSupportPackage : ApplicationAddOn
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
        //
        //  Support Package Type
        public enum SupportPackageType
        {
            HardwareBoard = 0,
            ThirdPartyLibrary,
            IODevice,
            CloudService,
            CodeGeneration,
            Connectivity
        }

        //
        //  Hardware Platform
        public enum HardwarePlatform
        {
            None = 0,
            Arduino,
            RaspberryPi,
            BeagleBoneBlack,
            Zynq,
            IntelSoC,
            AndroidDevice,
            AppleiOSDevice,
            ParrotDrone,
            NVIDIA_Jetson,
            TexasInstruments_C2000
        }

        //
        //  Communication Protocol
        public enum CommunicationProtocol
        {
            None = 0,
            Serial,
            I2C,
            SPI,
            WiFi,
            Bluetooth,
            Ethernet,
            USB,
            CAN,
            MQTT,
            ROS
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  MATLABSUPPORTPACKAGE CONSTRUCTOR
        //
        //  ************************************************************
        #region
        public MatlabSupportPackage()
        {
        }

        public MatlabSupportPackage(string name, SupportPackageType packageType)
        {
            Name = name;
            PackageType = packageType;
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
        public string InstallPath { get; set; } = string.Empty;
        public string SupportUrl { get; set; } = string.Empty;

        //
        //  Data
        public SupportPackageType PackageType { get; set; } = SupportPackageType.HardwareBoard;
        public HardwarePlatform TargetPlatform { get; set; } = HardwarePlatform.None;
        public new bool IsInstalled { get; set; } = false;
        public bool IsLicensed { get; set; } = false;

        //
        //  Communication
        public CommunicationProtocol PrimaryProtocol { get; set; } = CommunicationProtocol.None;
        public List<CommunicationProtocol> SupportedProtocols { get; set; } = new();

        //
        //  Hardware Connection
        public string PortName { get; set; } = string.Empty;
        public int BaudRate { get; set; } = 9600;
        public string IPAddress { get; set; } = string.Empty;
        public bool IsDeviceConnected { get; set; } = false;

        //
        //  Dependencies (toolboxes or other support packages required)
        public List<string> RequiredToolboxes { get; set; } = new();
        public List<string> RequiredSupportPackages { get; set; } = new();

        //
        //  Firmware / Driver
        public string FirmwareVersion { get; set; } = string.Empty;
        public string DriverVersion { get; set; } = string.Empty;
        public bool FirmwareUpdateAvailable { get; set; } = false;

        //
        //  Supported MATLAB Versions
        public string MinMatlabVersion { get; set; } = string.Empty;
        public string MaxMatlabVersion { get; set; } = string.Empty;

        //
        //  Owned & Owning Objects
        //
        //  Example / Demo Files
        public List<string> ExampleFiles { get; set; } = new();

        //
        //  Owning Matlab Application
        public MatlabApp? CurrentMatlabApp { get; set; }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region
        //
        //  Availability
        public bool IsAvailable()
        {
            return IsInstalled && IsLicensed;
        }

        public bool AreDependenciesMet(List<string> installedToolboxes,
            List<string> installedSupportPackages)
        {
            bool toolboxesMet = RequiredToolboxes.TrueForAll(
                t => installedToolboxes.Contains(t));
            bool packagesMet = RequiredSupportPackages.TrueForAll(
                p => installedSupportPackages.Contains(p));
            return toolboxesMet && packagesMet;
        }

        //
        //  Communication Protocol Management
        public void AddSupportedProtocol(CommunicationProtocol protocol)
        {
            if (!SupportedProtocols.Contains(protocol))
            {
                SupportedProtocols.Add(protocol);
            }
        }

        public bool SupportsProtocol(CommunicationProtocol protocol)
        {
            return SupportedProtocols.Contains(protocol);
        }

        //
        //  Dependency Management
        public void AddRequiredToolbox(string toolboxName)
        {
            if (!RequiredToolboxes.Contains(toolboxName))
            {
                RequiredToolboxes.Add(toolboxName);
            }
        }

        public void AddRequiredSupportPackage(string packageName)
        {
            if (!RequiredSupportPackages.Contains(packageName))
            {
                RequiredSupportPackages.Add(packageName);
            }
        }

        //
        //  Example File Management
        public void AddExampleFile(string filePath)
        {
            if (!ExampleFiles.Contains(filePath))
            {
                ExampleFiles.Add(filePath);
            }
        }

        public bool RemoveExampleFile(string filePath)
        {
            return ExampleFiles.Remove(filePath);
        }

        //
        //  Hardware Connection
        public bool ConfigureSerialConnection(string portName, int baudRate)
        {
            if (string.IsNullOrWhiteSpace(portName))
            {
                return false;
            }

            PortName = portName;
            BaudRate = baudRate;
            PrimaryProtocol = CommunicationProtocol.Serial;
            return true;
        }

        public bool ConfigureNetworkConnection(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            IPAddress = ipAddress;
            PrimaryProtocol = CommunicationProtocol.WiFi;
            return true;
        }

        //
        //  Installation Verification via MATLAB
        public bool VerifyInstallation(MatlabApp matlabApp)
        {
            try
            {
                //  Uses MATLAB's supportPackageInstaller or matlabshared.supportpkg to verify
                return matlabApp.RunMatlabCommand(
                    $"matlabshared.supportpkg.isInstalled('{Name}')");
            }
            catch
            {
                return false;
            }
        }

        //
        //  MATLAB Version Compatibility
        public bool IsCompatibleWithVersion(string matlabVersion)
        {
            if (string.IsNullOrEmpty(MinMatlabVersion) && string.IsNullOrEmpty(MaxMatlabVersion))
            {
                return true;
            }

            bool meetsMin = string.IsNullOrEmpty(MinMatlabVersion) ||
                string.Compare(matlabVersion, MinMatlabVersion, StringComparison.Ordinal) >= 0;
            bool meetsMax = string.IsNullOrEmpty(MaxMatlabVersion) ||
                string.Compare(matlabVersion, MaxMatlabVersion, StringComparison.Ordinal) <= 0;

            return meetsMin && meetsMax;
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
