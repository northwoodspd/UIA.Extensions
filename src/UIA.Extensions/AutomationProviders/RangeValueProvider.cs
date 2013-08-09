using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders
{
    public interface RangeValueControl
    {
        Control Control { get; }
        double Value { get; set; }
        bool IsReadOnly { get; }
        double Maximum { get; }
        double Minimum { get; }
        double LargeChange { get; }
        double SmallChange { get; }
    }

    public class RangeValueProvider : ControlProvider, IRangeValueProvider
    {
        private readonly RangeValueControl _rangeValue;

        public RangeValueProvider(RangeValueControl rangeValue) : base(rangeValue.Control)
        {
            _rangeValue = rangeValue;
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
