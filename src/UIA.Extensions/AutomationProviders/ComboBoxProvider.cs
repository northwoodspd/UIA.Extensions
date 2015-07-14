using System.Windows.Automation;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders
{
    public class ComboBoxProvider : ControlProvider
    {
        public ComboBoxProvider(ComboBoxInformation provider) : base(provider.Control)
        {
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.ComboBox.Id);
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, ControlType.ComboBox.LocalizedControlType);
        }
    }
}