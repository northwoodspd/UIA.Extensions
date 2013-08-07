using System.Windows.Automation.Provider;

namespace UIA.Fluent.AutomationProviders
{
    public class ChildProvider : AutomationProvider
    {
        private readonly AutomationProvider _parent;

        public ChildProvider(AutomationProvider parent)
        {
            _parent = parent;
        }

        public override IRawElementProviderFragmentRoot FragmentRoot
        {
            get { return _parent.FragmentRoot; }
        }

        protected override IRawElementProviderFragment Parent
        {
            get { return _parent; }
        }
    }
}