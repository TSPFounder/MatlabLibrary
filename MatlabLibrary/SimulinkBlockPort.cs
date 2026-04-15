using System;
using System.Collections.Generic;

namespace MatlabLib
{
    public class SimulinkBlockPort
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


        //  *****************************************************************************************
        //  ENUMERATIONS
        //
        //  ************************************************************
        #region
        //
        //  Port Direction
        public enum PortDirection
        {
            Input = 0,
            Output,
            Enable,
            Trigger,
            State,
            LConn,
            RConn
        }

        //
        //  Port Data Type
        public enum PortDataType
        {
            Double = 0,
            Single,
            Int8,
            Int16,
            Int32,
            Int64,
            UInt8,
            UInt16,
            UInt32,
            UInt64,
            Boolean,
            FixedPoint,
            Inherited
        }

        //
        //  Port Complexity
        public enum PortComplexity
        {
            Real = 0,
            Complex,
            Inherited
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  SIMULINKBLOCKPORT CONSTRUCTOR
        //
        //  ************************************************************
        #region
        public SimulinkBlockPort()
        {
        }

        public SimulinkBlockPort(string name, int portNumber, PortDirection direction)
        {
            Name = name;
            PortNumber = portNumber;
            Direction = direction;
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
        public string Description { get; set; } = string.Empty;

        //
        //  Data
        //
        //  Port Index (1-based, matching Simulink convention)
        public int PortNumber { get; set; } = 1;

        //
        //  Direction
        public PortDirection Direction { get; set; } = PortDirection.Input;

        //
        //  Data Type
        public PortDataType DataType { get; set; } = PortDataType.Inherited;

        //
        //  Complexity
        public PortComplexity Complexity { get; set; } = PortComplexity.Inherited;

        //
        //  Dimensions (e.g., [1] for scalar, [3,1] for vector, [2,3] for matrix)
        public int[] Dimensions { get; set; } = [1];

        //
        //  Sample Time (-1 = inherited)
        public double SampleTime { get; set; } = -1;

        //
        //  Units (e.g., "m/s", "rad", "N")
        public string Units { get; set; } = string.Empty;

        //
        //  Min/Max Constraints
        public double MinValue { get; set; } = double.NegativeInfinity;
        public double MaxValue { get; set; } = double.PositiveInfinity;

        //
        //  Direct Feedthrough (relevant for input ports in S-Functions and code generation)
        public bool HasDirectFeedthrough { get; set; } = true;

        //
        //  Owned & Owning Objects
        //
        //  Owning Block
        public SimulinkBlock? OwningBlock { get; set; }

        //
        //  Connected Signal
        public SimulinkSignal? ConnectedSignal { get; set; }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region
        //
        //  Connectivity
        public void Connect(SimulinkSignal signal)
        {
            ConnectedSignal = signal;
        }

        public void Disconnect()
        {
            ConnectedSignal = null;
        }

        public bool IsConnected()
        {
            return ConnectedSignal is not null;
        }

        //
        //  Dimension Helpers
        public bool IsScalar()
        {
            return Dimensions.Length == 1 && Dimensions[0] == 1;
        }

        public bool IsVector()
        {
            return Dimensions.Length == 1 && Dimensions[0] > 1;
        }

        public bool IsMatrix()
        {
            return Dimensions.Length == 2;
        }

        public int GetTotalElements()
        {
            int total = 1;
            foreach (int dim in Dimensions)
            {
                total *= dim;
            }
            return total;
        }

        //
        //  Validation
        public bool IsInput()
        {
            return Direction == PortDirection.Input;
        }

        public bool IsOutput()
        {
            return Direction == PortDirection.Output;
        }

        public bool IsValueInRange(double value)
        {
            return value >= MinValue && value <= MaxValue;
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
