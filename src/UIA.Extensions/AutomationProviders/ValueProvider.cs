using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders
{
    public class ValueProvider : ControlProvider, IValueProvider
    {
        private readonly ValueControl _valueControl;

        public ValueProvider(ValueControl valueControl) : base(valueControl.Control, ValuePattern.Pattern)
        {
            _valueControl = valueControl;
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