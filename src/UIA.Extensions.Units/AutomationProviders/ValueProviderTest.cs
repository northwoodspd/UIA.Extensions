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
                .Should().Be.SameAs(_valueProvider);
        }

        [Test]
        public void ValuesCanBeRetrieved()
        {
            _valueControl.Value = "Expected Value";
            _valueProvider.Value.Should().Equal("Expected Value");
        }

        [Test]
        public void ValuesCanBeSet()
        {
            _valueProvider.SetValue("The expected value to be set");
            _valueControl.Value.Should().Equal("The expected value to be set");
        }

        class ValueControlStub : ValueControl
        {
            public ValueControlStub(Control control) : base(control)
            { }

            public override string Value { get; set; }
        }
    }
}
