using MLApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public sealed class MatlabDesktopBackend 
{
    private readonly Type t;
    private readonly dynamic matlab;

    public MatlabDesktopBackend()
    {
        t = Type.GetTypeFromProgID("matlab.application")
            ?? throw new InvalidOperationException("MATLAB COM server not registered.");
        matlab = Activator.CreateInstance(t);

        matlab.Visible = 1;
    }
}
