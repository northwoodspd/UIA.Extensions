using System.Windows.Automation;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions.AutomationProviders
{
    [TestFixture]
    public class ValueProviderTest
    {
        private ValueControlStub _valueControl;
        private ValueProvider _valueProvider;

        [SetUp]
        public void SetUp()
        {
            _valueControl = new ValueControlStub(new Control());
            _valueProvider = new ValueProvider(_valueControl);
        }

        [Test]
        public void ItHasTheCorrectPattern()
        {
            _valueProvider.GetPatternProvider(ValuePatternIdentifiers.Pattern.Id)
                .Should().BeSameAs(_valueProvider);
        }

        [Test]
        public void ValuesCanBeRetrieved()
        {
            _valueControl.Value = "Expected Value";
            _valueProvider.Value.Should().BeEquivalentTo("Expected Value");
        }

        [Test]
        public void ValuesCanBeSet()
        {
            _valueProvider.SetValue("The expected value to be set");
            _valueControl.Value.Should().BeEquivalentTo("The expected value to be set");
        }

        class ValueControlStub : ValueControl
        {
            public ValueControlStub(Control control) : base(control)
            { }

            public override string Value { get; set; }
        }
    }
}
