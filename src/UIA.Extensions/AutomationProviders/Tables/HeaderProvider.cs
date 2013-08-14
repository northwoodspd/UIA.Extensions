using System.Collections.Generic;
using System.Windows.Automation;

namespace UIA.Extensions.AutomationProviders.Tables
{
    public class HeaderProvider : ChildProvider
    {
        public HeaderProvider(AutomationProvider automationProvider, IEnumerable<string> headers) : base(automationProvider)
        {
            foreach (var header in headers)
            {
                Children.Add(new HeaderItemProvider(this, header));
            }

            ControlType = ControlType.Header;
        }
    }
}