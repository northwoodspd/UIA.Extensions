using System;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Defaults
{
    public class NumericUpDownRangeValue : RangeValueControl
    {
        private readonly NumericUpDown _numericUpDown;

        public NumericUpDownRangeValue(NumericUpDown numericUpDown) : base(numericUpDown)
        {
            _numericUpDown = numericUpDown;
        }

        public override double Value
        {
            get { return Convert.ToDouble(_numericUpDown.Value); }
            set { _numericUpDown.Value = Convert.ToDecimal(value); }
        }

        public override bool IsReadOnly
        {
            get { return _numericUpDown.ReadOnly; }
        }

        public override double Maximum
        {
            get { return Convert.ToDouble(_numericUpDown.Maximum); }
        }

        public override double Minimum
        {
            get { return Convert.ToDouble(_numericUpDown.Minimum); }
        }

        public override double SmallChange
        {
            get { return Convert.ToDouble(_numericUpDown.Increment); }
        }

        public override double LargeChange { get; set; }
    }
}
