using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders
{
    public abstract class ValueControl
    {
        protected ValueControl(Control control)
        {
            Control = control;
        }

        public Control Control { get; private set; }
        public abstract string Value { get; set; }
        public virtual bool ReadOnly { get; set; }
    }

    public class ValueProvider : ControlProvider, IValueProvider
    {
        private readonly ValueControl _valueControl;

        public ValueProvider(ValueControl valueControl) : base(valueControl.Control)
        {
            _valueControl = valueControl;
        }

        protected override List<int> SupportedPatterns
        {
            get { return new List<int> { ValuePatternIdentifiers.Pattern.Id }; }
        }

        public void SetValue(string value)
        {
            _valueControl.Value = value;
        }

        public string Value
        {
            get { return _valueControl.Value; }
        }

        public bool IsReadOnly
        {
            get { return _valueControl.ReadOnly; }
        }
    }
}