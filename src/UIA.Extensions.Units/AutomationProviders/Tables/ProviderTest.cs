using System.Collections.Generic;
using System.Windows.Automation;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders.Tables
{
    abstract class ProviderTest<TProvider> where TProvider : AutomationProvider
    {
        [SetUp]
        public void Before()
        {
            Subject = Create();
        }

        protected TProvider Subject { get; private set; }

        protected int ControlTypeId
        {
            get { return Value<int>(AutomationElementIdentifiers.ControlTypeProperty.Id); }
        }

        protected string LocalizedControlType
        {
            get { return Value<string>(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id); }
        }

        protected abstract TProvider Create();

        protected TPattern Pattern<TPattern>(int id)
        {
            return (TPattern)Subject.GetPatternProvider(id);
        }

        protected List<AutomationProvider> Children
        {
            get { return Subject.Children; }
        }

        private TValue Value<TValue>(int id)
        {
            return (TValue)Subject.GetPropertyValue(id);
        }
    }
}