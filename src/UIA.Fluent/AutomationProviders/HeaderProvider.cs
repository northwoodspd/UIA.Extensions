using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace UIA.Fluent.AutomationProviders
{
    public class HeaderProvider : ChildProvider
    {
        public HeaderProvider(AutomationProvider automationProvider, IEnumerable<string> headers) : base(automationProvider)
        {
            Children.AddRange(headers.Select(x => new HeaderItemProvider(this, x)).Cast<ChildProvider>());
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
    }
}