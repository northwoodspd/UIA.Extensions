using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders
{
    public class ComboBoxProvider : ControlProvider, ISelectionProvider
    {
        private readonly ComboBoxInformation _provider;

        public ComboBoxProvider(ComboBoxInformation provider) : base(provider.Control, SelectionPattern.Pattern)
        {
            _provider = provider;
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, ControlType.ComboBox.Id);
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, ControlType.ComboBox.LocalizedControlType);
        }

        public IRawElementProviderSimple[] GetSelection()
        {
            throw new System.NotImplementedException();
        }

        public bool CanSelectMultiple
        {
            get { return _provider.CanSelectMultiple; }
        }

        public bool IsSelectionRequired
        {
            get { return _provider.IsRequired; }
        }
    }
}