using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class HeaderProvider : AutomationProvider
    {
        public HeaderProvider(AutomationProvider automationProvider, IEnumerable<HeaderInformation> headers) : base(automationProvider)
        {
            headers.ForEachWithIndex( (header, index) => AddChild(new HeaderItemProvider(this, header, index)));
            ControlType = ControlType.Header;
        }

        public override bool Equals(object obj)
        {
            return this.CeremoniallyEquals(obj, other => Children.SequenceEqual(other.Children));
        }

        public override int GetHashCode()
        {
            return Children.GetSequenceHashCode();
        }
    }
}