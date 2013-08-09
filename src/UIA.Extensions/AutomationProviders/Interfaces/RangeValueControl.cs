using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Interfaces
{
    public abstract class RangeValueControl
    {
        protected RangeValueControl(Control control)
        {
            Control = control;
        }

        public Control Control { get; private set; }
        public virtual double Value { get; set; }
        public virtual bool IsReadOnly { get; set; }
        public virtual double Maximum { get; set; }
        public virtual double Minimum { get; set; }
        public virtual double LargeChange { get; set; }
        public virtual double SmallChange { get; set; }
    }
}