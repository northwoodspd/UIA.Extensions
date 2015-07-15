using System.Windows.Automation;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders.Tables
{
    class ListItemProviderTest : ProviderTest<ListItemProvider>
    {
        private ListItemInformationStub _listItem;
        private ListProvider _provider;

        public ListItemProviderTest() : base(ControlType.ListItem)
        { }

        [Test]
        public void ItKnowsWhenSelected()
        {
            _listItem.SetSelected(true);

            Subject.IsSelected.Should().BeTrue();
        }

        [Test]
        public void ItCanBeSelected()
        {
            Subject.Select();

            _listItem.SelectWasCalled.Should().BeTrue();
        }

        [Test]
        public void ItCanAddToTheSelection()
        {
            Subject.AddToSelection();

            _listItem.AddToSelectionWasCalled.Should().BeTrue();
        }

        [Test]
        public void ItCanRemoveItselfFromSelection()
        {
            Subject.RemoveFromSelection();

            _listItem.RemoveFromSelectionWasCalled.Should().BeTrue();
        }

        protected override ListItemProvider Create()
        {
            _provider = new ListProvider(new ListInformationStub());
            _listItem = ListItemInformationStub.Create("expected item") as ListItemInformationStub;
            return new ListItemProvider(_provider, _listItem);
        }
    }

    internal class ListItemInformationStub : ListItemInformation
    {
        private ListItemInformationStub(string text) : base(text)
        { }

        public void SetSelected(bool isSelected)
        {
            IsSelected = isSelected;
        }

        public bool SelectWasCalled { get; private set; }
        public bool AddToSelectionWasCalled { get; private set; }
        public bool RemoveFromSelectionWasCalled { get; private set; }

        public override void Select()
        {
            SelectWasCalled = true;
        }

        public override void AddToSelection()
        {
            AddToSelectionWasCalled = true;
        }

        public override void RemoveFromSelection()
        {
            RemoveFromSelectionWasCalled = true;
        }

        public static ListItemInformation Create(string text)
        {
            return new ListItemInformationStub(text);
        }
    }
}