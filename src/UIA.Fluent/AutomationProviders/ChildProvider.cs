using System.Windows.Automation.Provider;

namespace UIA.Fluent.AutomationProviders
{
    public class ChildProvider : AutomationProvider
    {
        private readonly AutomationProvider _automationProvider;

        public ChildProvider(AutomationProvider automationProvider)
        {
            _automationProvider = automationProvider;
        }

        public override IRawElementProviderFragmentRoot FragmentRoot
        {
            get { return _automationProvider.FragmentRoot; }
        }

        protected override IRawElementProviderFragment Parent
        {
            get { return _automationProvider; }
        }
    }
}