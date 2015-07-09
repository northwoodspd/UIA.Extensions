using System.Collections.Generic;
using System.Linq;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public abstract class RowInformation
    {
        public abstract string Value { get; }
        public abstract List<CellInformation> Cells { get; }
        public abstract void Select();
        public abstract void AddToSelection();
        public abstract void ClearSelection();
        public abstract bool IsSelected { get; }
        public abstract bool IsVisible { get; set; }

        public override bool Equals(object obj)
        {
            return this.CeremoniallyEquals(obj, other => Equals(Value, other.Value) && Cells.SequenceEqual(other.Cells));
        }

        public override int GetHashCode()
        {
            return this.CombinedHashCodes(Value, Cells.GetSequenceHashCode());
        }
    }
}