using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders;

namespace UIA.Extensions.TestApplication.Implementations
{
    internal class TextProvider :  AutomationProvider, IValueProvider
    {
        public TextProvider() : base(ValuePatternIdentifiers.Pattern)
        {}

        public override string Name
        {
            get { return Value; }
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
        public bool IsReadOnly { get; private set; }
    }
}