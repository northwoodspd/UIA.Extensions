using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders
{
    public class RangeValueProvider : ControlProvider
    {
        public RangeValueProvider(Control control) : base(control)
        {
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { RangeValuePatternIdentifiers.Pattern.Id }; }
        }
    }
}
