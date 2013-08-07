using System.Windows.Automation;

namespace UIA.Fluent.AutomationProviders.Tables
{
    public class HeaderItemProvider : ChildProvider
    {
        public HeaderItemProvider(AutomationProvider headerProvider, string header) : base(headerProvider)
        {
            Name = header;
        }

        protected override int ControlTypeId
        {
            get { return ControlType.HeaderItem.Id; }
        }
    }
}