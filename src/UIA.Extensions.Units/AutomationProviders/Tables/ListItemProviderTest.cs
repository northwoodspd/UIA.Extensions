using System.Windows.Automation;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders.Tables
{
    class ListItemProviderTest : ProviderTest<ListItemProvider>
    {
        private ListItemInformation _listItem;
        private ListProvider _provider;

        public ListItemProviderTest() : base(ControlType.ListItem)
        { }

        protected override ListItemProvider Create()
        {
            _provider = new ListProvider(new ListInformationStub());
            _listItem = new ListItemInformation("expected item");
            return new ListItemProvider(_provider, _listItem);
        }
    }
}