using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders.Tables
{
    class ComboBoxProviderTest
    {
        private ComboBoxInformationStub _comboInformation;
        private ComboBoxProvider _comboProvider;

        private ISelectionProvider Selection
        {
            get { return (ISelectionProvider) _comboProvider.GetPatternProvider(SelectionPatternIdentifiers.Pattern.Id); }
        }

        [SetUp]
        public void SetUp()
        {
            _comboInformation = new ComboBoxInformationStub(new Control());
            _comboProvider = new ComboBoxProvider(_comboInformation);
        }

        [Test]
        public void ItHasTheCorrectControlType()
        {
            _comboProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Be(ControlType.ComboBox.Id);
        }

        [Test]
        public void ItLocalizesTheControlType()
        {
            _comboProvider.GetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
                .Should().Be(ControlType.ComboBox.LocalizedControlType);
        }

        [Test]
        public void ItReportsAsSelectionPattern()
        {
            _comboProvider.GetPatternProvider(SelectionPatternIdentifiers.Pattern.Id)
                .Should().BeSameAs(_comboProvider);
        }

        [Test]
        public void ItCanReportIfSelectionIsRequired()
        {
            _comboInformation.SetIsRequired(true);

            Selection.IsSelectionRequired.Should().BeTrue();
        }
    }

    internal class ComboBoxInformationStub : ComboBoxInformation
    {
        public ComboBoxInformationStub(Control control) : base(control)
        { }

        public void SetIsRequired(bool isRequired)
        {
            IsRequired = isRequired;
        }
    }
}