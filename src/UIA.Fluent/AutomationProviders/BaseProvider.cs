using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders
{
    [ComVisible(true)]
    public abstract class BaseProvider : IRawElementProviderFragmentRoot
    {
        private readonly Control _control;
        private readonly Dictionary<int, object> _properties;

        protected virtual int ControlTypeId
        {
            get { return ControlType.Custom.Id; }
        }

        protected BaseProvider(Control control)
        {
            _control = control;
            _properties = new Dictionary<int, object>
                              {
                                  {AutomationElementIdentifiers.ControlTypeProperty.Id, ControlTypeId}, 
                                  {AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, _control.GetType().FullName}, 
                                  {AutomationElementIdentifiers.AutomationIdProperty.Id, _control.Name}, 
                                  {AutomationElementIdentifiers.IsKeyboardFocusableProperty.Id, true}, 
                              };
        }

        protected abstract List<int> SupportedPatterns { get; }

        public const int ProviderUseComThreading = 0x20;
        public ProviderOptions ProviderOptions
        {
            get
            {
                return (ProviderOptions)((int)ProviderOptions.ServerSideProvider | ProviderUseComThreading);
            }
        }

        public IRawElementProviderSimple HostRawElementProvider
        {
            get { return AutomationInteropProvider.HostProviderFromHandle(_control.Handle); }
        }

        public Rect BoundingRectangle { get; private set; }

        public IRawElementProviderFragmentRoot FragmentRoot
        {
            get { return this; }
        }

        public object GetPropertyValue(int propertyId)
        {
            return _properties.Where(x => x.Key.Equals(propertyId))
                              .Select(x => x.Value)
                              .FirstOrDefault();
        }

        public void SetPropertyValue(int propertyId, Object propertyValue)
        {
            _properties[propertyId] = propertyValue;
        }

        public object GetPatternProvider(int patternId)
        {
            return SupportedPatterns.Contains(patternId) ? this : null;
        }

        public IRawElementProviderSimple[] GetEmbeddedFragmentRoots()
        {
            return null;
        }

        public int[] GetRuntimeId()
        {
            return new[] { _control.GetHashCode() };
        }

        public void SetFocus()
        {
        }

        public IRawElementProviderFragment Navigate(NavigateDirection direction)
        {
            switch (direction)
            {
               case NavigateDirection.FirstChild:
                    return FirstChild;
                case NavigateDirection.LastChild:
                    return LastChild;
                case NavigateDirection.NextSibling:
                    return NextSibling;
                case NavigateDirection.PreviousSibling:
                    return PreviousSibling;
                case NavigateDirection.Parent:
                    return Parent;
                default:
                    return null;
            }
        }

        protected virtual IRawElementProviderFragment LastChild
        {
            get { return null; }
        }

        protected virtual IRawElementProviderFragment FirstChild
        {
            get { return null; }
        }

        protected virtual IRawElementProviderFragment NextSibling
        {
            get { return null; }
        }

        protected virtual IRawElementProviderFragment PreviousSibling
        {
            get { return null; }
        }


        protected virtual IRawElementProviderFragment Parent
        {
            get { return null; }
        }

        public IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
        {
            return null;
        }

        public IRawElementProviderFragment GetFocus()
        {
            return null;
        }
    }
}
