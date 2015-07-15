using System.Collections.Generic;
using System.Windows.Automation;
using FluentAssertions;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders.Tables
{
    abstract class ProviderTest<TProvider> where TProvider : AutomationProvider
    {
        private readonly ControlType _expectedControlType;

        protected ProviderTest(ControlType controlType)
        {
            _expectedControlType = controlType;
        }

        [SetUp]
        public void Before()
        {
            Subject = Create();
        }

        [Test]
        public void ItHasTheCorrectControlType()
        {
            Value<int>(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Be(_expectedControlType.Id);
        }

        [Test]
        public void ItLocalizesTheControlType()
        {
            Value<string>(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
                .Should().Be(_expectedControlType.LocalizedControlType);
        }

        protected TProvider Subject { get; private set; }

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