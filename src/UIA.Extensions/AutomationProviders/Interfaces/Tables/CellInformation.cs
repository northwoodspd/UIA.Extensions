using System.Windows;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public abstract class CellInformation
    {
        public abstract string Value { get; }
        public abstract int Row { get;  }
        public abstract int Column { get;  }
        public abstract Rect Location { get; }

        public override bool Equals(object obj)
        {
            return this.CeremoniallyEquals(obj, FieldsEqual);
        }

        public override int GetHashCode()
        {
            return this.CombinedHashCodes(Value, Column, Row);
        }

        private bool FieldsEqual(CellInformation other)
        {
            return Equals(Value, other.Value)
                   && Equals(Column, other.Column)
                   && Equals(Row, other.Row);
        }
    }
}