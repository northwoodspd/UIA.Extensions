using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders.Tables
{
    class ListProviderTest
    {
        private ListInformationStub _listInformation;
        private ListProvider _listProvider;

        private ISelectionProvider Selection
        {
            get { return (ISelectionProvider) _listProvider.GetPatternProvider(SelectionPatternIdentifiers.Pattern.Id); }
        }

        [SetUp]
        public void SetUp()
        {
            _listInformation = new ListInformationStub(new Control());
            _listProvider = new ListProvider(_listInformation);
        }

        [Test]
        public void ItHasTheCorrectControlType()
        {
            _listProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Be(ControlType.List.Id);
        }

        [Test]
        public void ItLocalizesTheControlType()
        {
            _listProvider.GetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
                .Should().Be(ControlType.List.LocalizedControlType);
        }

        [Test]
        public void ItReportsAsSelectionPattern()
        {
            _listProvider.GetPatternProvider(SelectionPatternIdentifiers.Pattern.Id)
                .Should().BeSameAs(_listProvider);
        }

        [Test]
        public void ItCanReportIfSelectionIsRequired()
        {
            _listInformation.SetIsRequired(true);

            Selection.IsSelectionRequired.Should().BeTrue();
        }

        [Test]
        public void ItCanReportIfMultipleSelectionIsPossible()
        {
            _listInformation.SetCanSelectMultiple(true);

            Selection.CanSelectMultiple.Should().BeTrue();
        }
    }

    internal class ListInformationStub : ListInformation
    {
        public ListInformationStub(Control control) : base(control)
        { }

        public void SetIsRequired(bool isRequired)
        {
            IsRequired = isRequired;
        }

        public void SetCanSelectMultiple(bool canSelectMultiple)
        {
            CanSelectMultiple = canSelectMultiple;
        }
    }
}