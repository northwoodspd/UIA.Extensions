using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders
{
    [ComVisible(true)]
    public class AutomationProvider : IRawElementProviderFragmentRoot
    {
        private readonly Dictionary<int, Func<object>> _properties;

        public AutomationProvider()
        {
            _properties = new Dictionary<int, Func<object>>();
            SetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id, () => ControlType.Id);
            SetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id, () => ControlType.LocalizedControlType);
            SetPropertyValue(AutomationElementIdentifiers.IsKeyboardFocusableProperty.Id, true);
            SetPropertyValue(AutomationElementIdentifiers.NameProperty.Id, () => Name);
            SetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id, () => Id);

            _children = new List<AutomationProvider>();
            _supportedPatterns = new List<int>();

            ControlType = ControlType.Custom;
        }

        public AutomationProvider(AutomationProvider parent) : this()
        {
            Parent = parent;
        }

        protected AutomationProvider(params AutomationPattern[] patterns) : this()
        {
            patterns.ForEach(x => _supportedPatterns.Add(x.Id));
        }

        public virtual ControlType ControlType { get; set; }
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }

        private List<int> _supportedPatterns; 
        protected virtual List<int> SupportedPatterns { get { return _supportedPatterns; } }

        public const int ProviderUseComThreading = 0x20;

        public ProviderOptions ProviderOptions
        {
            get
            {
                return (ProviderOptions)((int)ProviderOptions.ServerSideProvider | ProviderUseComThreading);
            }
        }

        public virtual IRawElementProviderSimple HostRawElementProvider
        {
            get { return null; }
        }

        public virtual Rect BoundingRectangle { get; private set; }

        public virtual IRawElementProviderFragmentRoot FragmentRoot
        {
            get { return Parent.FragmentRoot ?? this; }
        }

        public object GetPropertyValue(int propertyId)
        {
            var propertyGetter = _properties.Where(x => x.Key.Equals(propertyId))
                .Select(x => x.Value)
                .FirstOrDefault();
            return null == propertyGetter ? null : propertyGetter();
        }

        public void SetPropertyValue(int propertyId, Object propertyValue)
        {
            _properties[propertyId] = () => propertyValue;
        }

        public void SetPropertyValue(int propertyId, Func<Object> propertyGetter)
        {
            _properties[propertyId] = propertyGetter;
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
            return new[] { GetHashCode() };
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
            get { return Children.LastOrDefault(); }
        }

        protected virtual IRawElementProviderFragment FirstChild
        {
            get { return Children.FirstOrDefault(); }
        }

        protected virtual IRawElementProviderFragment NextSibling
        {
            get { return Parent.Children.After(this); }
        }

        protected virtual IRawElementProviderFragment PreviousSibling
        {
            get { return Parent.Children.Before(this); }
        }


        public virtual AutomationProvider Parent { get; set; }

        private readonly List<AutomationProvider> _children = new List<AutomationProvider>();

        public virtual List<AutomationProvider> Children
        {
            get { return _children; }
        }

        public virtual void AddChild(AutomationProvider child)
        {
            child.Parent = this;
            Children.Add(child);
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