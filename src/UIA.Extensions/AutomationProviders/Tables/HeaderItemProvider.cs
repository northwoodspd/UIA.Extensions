using System.Windows.Automation;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class HeaderItemProvider : AutomationProvider
    {
        public HeaderItemProvider(AutomationProvider headerProvider, string header, int index) : base(headerProvider)
        {
            Index = index;
            Name = header;
            ControlType = ControlType.HeaderItem;
        }

        public override bool Equals(object obj)
        {
            return this.CeremoniallyEquals(obj, other => Equals(Index, other.Index) && Equals(Name, other.Name));
        }

        public override int GetHashCode()
        {
            return this.CombinedHashCodes(Name, Index);
        }

        protected int Index { get; private set; }
    }
}