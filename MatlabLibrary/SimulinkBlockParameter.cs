using System;
using System.Collections.Generic;
using System.Text;

namespace MatlabLib
{
    public class SimulinkBlockParameter
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
        //  Parameter Data Type
        public enum ParameterDataType
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
            String,
            Enum,
            FixedPoint,
            Expression,
            Inherited
        }

        //
        //  Parameter Evaluation Method
        public enum ParameterEvaluation
        {
            Literal = 0,
            WorkspaceVariable,
            MatlabExpression
        }

        //
        //  Parameter Visibility
        public enum ParameterVisibility
        {
            Visible = 0,
            Hidden,
            ReadOnly
        }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  SIMULINKBLOCKPARAMETER CONSTRUCTOR
        //
        //  ************************************************************
        #region
        public SimulinkBlockParameter()
        {
        }

        public SimulinkBlockParameter(string name, string value)
        {
            Name = name;
            Value = value;
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
        public string Prompt { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //
        //  Data
        //
        //  Value (string representation; may be a literal, variable name, or expression)
        public string Value { get; set; } = string.Empty;

        //
        //  Default Value
        public string DefaultValue { get; set; } = string.Empty;

        //
        //  Data Type
        public ParameterDataType DataType { get; set; } = ParameterDataType.Double;

        //
        //  Evaluation Method
        public ParameterEvaluation Evaluation { get; set; } = ParameterEvaluation.Literal;

        //
        //  Visibility
        public ParameterVisibility Visibility { get; set; } = ParameterVisibility.Visible;

        //
        //  Minimum and Maximum constraints
        public double MinValue { get; set; } = double.NegativeInfinity;
        public double MaxValue { get; set; } = double.PositiveInfinity;

        //
        //  Units (e.g., "m/s", "rad", "N")
        public string Units { get; set; } = string.Empty;

        //
        //  Is Tunable (can be changed during simulation without recompiling)
        public bool IsTunable { get; set; } = true;

        //
        //  Is Read-Only
        public bool IsReadOnly { get; set; } = false;

        //
        //  Allowed Enumeration Values (when DataType is Enum)
        public List<string> EnumValues { get; set; } = new();

        //
        //  Callback (MATLAB expression executed when parameter value changes)
        public string Callback { get; set; } = string.Empty;

        //
        //  Tooltip
        public string Tooltip { get; set; } = string.Empty;

        //
        //  Tab (dialog tab grouping in the block mask)
        public string DialogTab { get; set; } = string.Empty;

        //
        //  Owned & Owning Objects
        //
        //  Owning Block
        public SimulinkBlock? OwningBlock { get; set; }
        #endregion
        //  *****************************************************************************************


        //  *****************************************************************************************
        //  METHODS
        //
        //  ************************************************************
        #region
        //
        //  Value Management
        public void SetValue(string newValue)
        {
            if (!IsReadOnly)
            {
                Value = newValue;
            }
        }

        public void ResetToDefault()
        {
            if (!IsReadOnly)
            {
                Value = DefaultValue;
            }
        }

        public bool HasCustomValue()
        {
            return !string.Equals(Value, DefaultValue, StringComparison.Ordinal);
        }

        //
        //  Validation
        public bool IsWithinRange(double numericValue)
        {
            return numericValue >= MinValue && numericValue <= MaxValue;
        }

        public bool Validate()
        {
            //  Check enum constraint
            if (DataType == ParameterDataType.Enum && EnumValues.Count > 0)
            {
                return EnumValues.Contains(Value);
            }

            //  Check numeric range constraint
            if (DataType is ParameterDataType.Double or ParameterDataType.Single
                or ParameterDataType.Int8 or ParameterDataType.Int16
                or ParameterDataType.Int32 or ParameterDataType.Int64
                or ParameterDataType.UInt8 or ParameterDataType.UInt16
                or ParameterDataType.UInt32 or ParameterDataType.UInt64)
            {
                if (double.TryParse(Value, out double numericValue))
                {
                    return IsWithinRange(numericValue);
                }

                //  Allow workspace variable or expression references
                return Evaluation != ParameterEvaluation.Literal;
            }

            return true;
        }

        public bool IsNumericType()
        {
            return DataType is ParameterDataType.Double or ParameterDataType.Single
                or ParameterDataType.Int8 or ParameterDataType.Int16
                or ParameterDataType.Int32 or ParameterDataType.Int64
                or ParameterDataType.UInt8 or ParameterDataType.UInt16
                or ParameterDataType.UInt32 or ParameterDataType.UInt64
                or ParameterDataType.FixedPoint;
        }

        //
        //  Enum Management
        public void AddEnumValue(string enumValue)
        {
            if (!EnumValues.Contains(enumValue))
            {
                EnumValues.Add(enumValue);
            }
        }

        public bool RemoveEnumValue(string enumValue)
        {
            return EnumValues.Remove(enumValue);
        }

        //
        //  Display
        public string GetDisplayText()
        {
            return string.IsNullOrEmpty(Prompt) ? Name : Prompt;
        }

        public override string ToString()
        {
            return $"{Name} = {Value}";
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
