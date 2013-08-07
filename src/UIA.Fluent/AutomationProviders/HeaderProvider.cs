using System.Windows.Automation;

namespace UIA.Fluent.AutomationProviders
{
    public class HeaderProvider : ChildProvider
    {
        public HeaderProvider(AutomationProvider parent) : base(parent)
        {}

        protected override int ControlTypeId
        {
            get { return ControlType.Header.Id; }
        }
    }
}