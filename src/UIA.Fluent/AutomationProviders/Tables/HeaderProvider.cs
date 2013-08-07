using System.Collections.Generic;
using System.Windows.Automation;

namespace UIA.Fluent.AutomationProviders
{
    public class HeaderProvider : ChildProvider
    {
        public HeaderProvider(AutomationProvider automationProvider, IEnumerable<string> headers) : base(automationProvider)
        {
            foreach (var header in headers)
            {
                Children.Add(new HeaderItemProvider(this, header));
            }
        }

        protected override int ControlTypeId
        {
            get { return ControlType.Header.Id; }
        }
    }
}