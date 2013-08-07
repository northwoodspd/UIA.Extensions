using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Fluent.Extensions;

namespace UIA.Fluent.AutomationProviders
{
    public class HeaderProvider : ChildProvider
    {
        public HeaderProvider(AutomationProvider automationProvider, IEnumerable<string> headers) : base(automationProvider)
        {
            foreach (var header in headers)
            {
                Children.Add(new HeaderItemProvider(this, header));
            }
        }

        protected override int ControlTypeId
        {
            get { return ControlType.Header.Id; }
        }
    }

    public class HeaderItemProvider : ChildProvider
    {
        private readonly HeaderProvider _headerProvider;

        public HeaderItemProvider(HeaderProvider headerProviderProvider, string header) : base(headerProviderProvider)
        {
            _headerProvider = headerProviderProvider;
            Name = header;
        }

        protected override IRawElementProviderFragment NextSibling
        {
            get { return _headerProvider.Children.After(this); }
        }

        protected override IRawElementProviderFragment PreviousSibling
        {
            get { return _headerProvider.Children.Before(this); }
        }
    }
}