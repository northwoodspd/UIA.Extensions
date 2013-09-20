using System.Collections.Generic;
using System.Linq;

namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public abstract class RowInformation
    {
        public abstract string Value { get; }
        public abstract List<CellInformation> Cells { get; }
        public abstract void Select();
        public abstract bool IsSelected { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            var other = (RowInformation)obj;
            return Equals(Value, other.Value) && Cells.SequenceEqual(other.Cells);
        }
    }
}