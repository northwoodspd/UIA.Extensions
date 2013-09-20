using System.Windows.Automation;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class HeaderItemProvider : ChildProvider
    {
        public HeaderItemProvider(AutomationProvider headerProvider, string header) : base(headerProvider)
        {
            Name = header;
            ControlType = ControlType.HeaderItem;
        }

        public override bool Equals(object obj)
        {
            return this.CeremoniallyEquals(obj, (other) => Equals(Name, other.Name));
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}