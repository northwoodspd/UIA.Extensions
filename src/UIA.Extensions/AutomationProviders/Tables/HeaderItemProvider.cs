using System.Windows.Automation;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class HeaderItemProvider : AutomationProvider
    {
        public HeaderItemProvider(AutomationProvider headerProvider, HeaderInformation header, int index) : base(headerProvider)
        {
            header = header ?? HeaderInformation.NullHeader;

            Index = index;
            Name = header.Text;
            ControlType = ControlType.HeaderItem;

            SetPropertyValue(AutomationElementIdentifiers.IsOffscreenProperty.Id, () => !header.IsVisible);
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