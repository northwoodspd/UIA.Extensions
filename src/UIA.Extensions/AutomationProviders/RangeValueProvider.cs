using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders
{
    public class RangeValueProvider : ControlProvider, IRangeValueProvider
    {
        private readonly RangeValueControl _rangeValue;

        public RangeValueProvider(RangeValueControl rangeValue) : base(rangeValue.Control)
        {
            _rangeValue = rangeValue;
            ControlType = ControlType.Spinner;
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { RangeValuePatternIdentifiers.Pattern.Id }; }
        }

        public void SetValue(double value)
        {
            _rangeValue.Value = value;
        }

        public double Value
        {
            get { return _rangeValue.Value; }
        }

        public bool IsReadOnly
        {
            get { return _rangeValue.IsReadOnly; }
        }

        public double Maximum
        {
            get { return _rangeValue.Maximum; }
        }

        public double Minimum
        {
            get { return _rangeValue.Minimum; }
        }

        public double LargeChange
        {
            get { return _rangeValue.LargeChange; }
        }

        public double SmallChange
        {
            get { return _rangeValue.SmallChange; }
        }
    }
}
