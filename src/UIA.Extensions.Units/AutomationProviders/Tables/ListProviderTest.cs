using System.Collections.Generic;
using System.Linq;
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
            get { return (ISelectionProvider)_listProvider.GetPatternProvider(SelectionPatternIdentifiers.Pattern.Id); }
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

        [Test]
        public void ItCanHaveChildren()
        {
            _listInformation.AddItems("First", "Second", "Third");

            _listProvider.Children.Select(x => x.GetPropertyValue(AutomationElementIdentifiers.NameProperty.Id).ToString())
                .ShouldBeEquivalentTo(new[] { "First", "Second", "Third" });
        }
    }

    internal class ListInformationStub : ListInformation
    {
        private List<ListItemInformation> _items;

        public ListInformationStub(Control control)
            : base(control)
        { }

        public void SetIsRequired(bool isRequired)
        {
            IsRequired = isRequired;
        }

        public void SetCanSelectMultiple(bool canSelectMultiple)
        {
            CanSelectMultiple = canSelectMultiple;
        }

        public void AddItems(params string[] items)
        {
            _items = items.Select(x => new ListItemInformation(x)).ToList();
        }

        public override List<ListItemInformation> ListItems
        {
            get { return _items ?? new List<ListItemInformation>(); }
        }
    }
}