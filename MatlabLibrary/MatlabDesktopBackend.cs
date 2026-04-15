using System.Windows;
using System.Collections.Generic;
using Applications;
using MLApp;
using SE_Library;
using System.Runtime.Versioning;



[SupportedOSPlatform("windows")]
public sealed class MatlabDesktopBackend 
{
    private readonly Type t;
    private readonly dynamic matlab;

    public MatlabDesktopBackend()
    {
        t = Type.GetTypeFromProgID("matlab.application")
            ?? throw new InvalidOperationException("MATLAB COM server not registered.");
        var instance = Activator.CreateInstance(t)
            ?? throw new InvalidOperationException("Failed to create MATLAB COM instance.");
        matlab = instance;

        matlab.Visible = 1;
    }
}
