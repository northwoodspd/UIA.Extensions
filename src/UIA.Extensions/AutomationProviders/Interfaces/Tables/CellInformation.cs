using System.Windows;

namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public abstract class CellInformation
    {
        public abstract string Value { get; }
        public abstract int Row { get;  }
        public abstract int Column { get;  }
        public abstract Rect Location { get; }

        protected bool Equals(CellInformation other)
        {
            return Equals(Value, other.Value)
                   && Equals(Column, other.Column)
                   && Equals(Row, other.Row);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CellInformation) obj);
        }
    }
}