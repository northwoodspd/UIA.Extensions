using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders
{
    public class ComboBoxProvider : ControlProvider, ISelectionProvider
    {
        public ComboBoxProvider(ComboBoxInformation provider) : base(provider.Control, SelectionPattern.Pattern)
        {
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.ComboBox.Id);
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, ControlType.ComboBox.LocalizedControlType);
        }

        public IRawElementProviderSimple[] GetSelection()
        {
            throw new System.NotImplementedException();
        }

        public bool CanSelectMultiple { get; private set; }
        public bool IsSelectionRequired { get; private set; }
    }
}