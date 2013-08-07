using System.Windows.Automation.Provider;
using UIA.Fluent.Extensions;

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

        protected override IRawElementProviderFragment NextSibling
        {
            get { return _parent.Children.After(this); }
        }

        protected override IRawElementProviderFragment PreviousSibling
        {
            get { return _parent.Children.Before(this); }
        }
    }
}