using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders
{
    public class RangeValueProvider : ControlProvider, IRangeValueProvider
    {
        public RangeValueProvider(Control control) : base(control)
        {
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { RangeValuePatternIdentifiers.Pattern.Id }; }
        }

        public void SetValue(double value)
        {
            throw new System.NotImplementedException();
        }

        public double Value { get; private set; }
        public bool IsReadOnly { get; private set; }
        public double Maximum { get; private set; }
        public double Minimum { get; private set; }
        public double LargeChange { get; private set; }
        public double SmallChange { get; private set; }
    }
}
