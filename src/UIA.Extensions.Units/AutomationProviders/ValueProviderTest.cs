using System;
using System.Windows.Automation;
using System.Windows.Forms;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Extensions.AutomationProviders
{
    [TestFixture]
    public class ValueProviderTest
    {

        [Test]
        public void ItHasTheCorrectPattern()
        {
            var valueProvider = GetValueProvider();

            valueProvider.GetPatternProvider(ValuePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(valueProvider);
        }

        [Test]
        public void ValuesCanBeRetrieved()
        {
            GetValueProvider(() => "Expected Value").Value
                .Should().Equal("Expected Value");
        }

        [Test]
        public void ValuesCanBeSet()
        {
            var expectedValue = string.Empty;

            GetValueProvider(null, x => expectedValue = x)
                .SetValue("The expected value to be set");

            expectedValue.Should().Equal("The expected value to be set");
        }

        private ValueProvider GetValueProvider(Func<string> getter = null, Action<string> setter = null)
        {
            return new ValueProvider(new Control(), getter, setter);
        }
    }
}
