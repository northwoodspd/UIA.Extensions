using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders.Interfaces
{
    [TestFixture]
    class RangeValueProviderTest
    {
        private RangeValueProvider _provider;

        [SetUp]
        public void SetUp()
        {
            _provider = new RangeValueProvider(new RangeValueControlStub(new Control()));
        }

        [Test]
        public void ItHasTheCorrectPattern()
        {
            _provider.GetPatternProvider(RangeValuePatternIdentifiers.Pattern.Id)
                .Should().BeSameAs(_provider);
        }

        [Test]
        public void ItIsOfTypeSpinner()
        {
            _provider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .ShouldBeEquivalentTo(ControlType.Spinner.Id);
        }
        
        [Test]
        public void ItCanDoWhatItSaysItCanDo()
        {
            _provider.Should().BeAssignableTo<IRangeValueProvider>();
        }

        [TestFixture]
        class RangeValueImplementation
        {
            private RangeValueProvider _provider;
            private RangeValueControlStub _rangeValue;

            [SetUp]
            public void SetUp()
            {
                _rangeValue = new RangeValueControlStub(new Control());
                _provider = new RangeValueProvider(_rangeValue);
            }

            [Test]
            public void ItHasValue()
            {
                _rangeValue.Value = 7.2;
                _provider.Value.ShouldBeEquivalentTo(7.2);
            }

            [Test]
            public void ItCanSetValues()
            {
                _provider.SetValue(123.0);
                _rangeValue.Value.ShouldBeEquivalentTo(123.0);
            }

            [Test]
            public void ItKnowsIfItIsReadOnly()
            {
                _rangeValue.IsReadOnly = true;
                _provider.IsReadOnly.Should().BeTrue();
            }

            [Test]
            public void ItKnowsTheMinimum()
            {
                _rangeValue.Minimum = 7.2;
                _provider.Minimum.ShouldBeEquivalentTo(7.2);
            }

            [Test]
            public void ItKnowsTheMaximum()
            {
                _rangeValue.Maximum = 7.2;
                _provider.Maximum.ShouldBeEquivalentTo(7.2);
            }

            [Test]
            public void ItKnowsLargeChangeIncrements()
            {
                _rangeValue.LargeChange = 2.0;
                _provider.LargeChange.ShouldBeEquivalentTo(2.0);
            }

            [Test]
            public void ItKnowsSmallChangeIncrements()
            {
                _rangeValue.SmallChange = 1.0;
                _provider.SmallChange.ShouldBeEquivalentTo(1.0);
            }
        }

        class RangeValueControlStub : RangeValueControl
        {
            public RangeValueControlStub(Control control) : base(control)
            {
            }

            public override double Value { get; set; }
            public override bool IsReadOnly { get; set; }
            public override double Maximum { get; set; }
            public override double Minimum { get; set; }
            public override double LargeChange { get; set; }
            public override double SmallChange { get; set; }
        }
    }
}
