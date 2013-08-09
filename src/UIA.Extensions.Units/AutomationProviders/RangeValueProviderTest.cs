using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Extensions.AutomationProviders
{
    [TestFixture]
    class RangeValueProviderTest
    {
        private RangeValueProvider _provider;

        [SetUp]
        public void SetUp()
        {
            _provider = new RangeValueProvider(new RangeValueControlStub());
        }

        [Test]
        public void ItHasTheCorrectPattern()
        {
            _provider.GetPatternProvider(RangeValuePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_provider);
        }
        
        [Test]
        public void ItCanDoWhatItSaysItCanDo()
        {
            _provider.Should().Be.AssignableFrom<IRangeValueProvider>();
        }

        [TestFixture]
        class RangeValueImplementation
        {
            private RangeValueProvider _provider;
            private RangeValueControlStub _rangeValue;

            [SetUp]
            public void SetUp()
            {
                _rangeValue = new RangeValueControlStub();
                _provider = new RangeValueProvider(_rangeValue);
            }

            [Test]
            public void ItHasValue()
            {
                _rangeValue.Value = 7.2;
                _provider.Value.Should().Equal(7.2);
            }

            [Test]
            public void ItCanSetValues()
            {
                _provider.SetValue(123.0);
                _rangeValue.Value.Should().Equal(123.0);
            }

            [Test]
            public void ItKnowsIfItIsReadOnly()
            {
                _rangeValue.IsReadOnly = true;
                _provider.IsReadOnly.Should().Be.True();
            }

            [Test]
            public void ItKnowsTheMinimum()
            {
                _rangeValue.Minimum = 7.2;
                _provider.Minimum.Should().Equal(7.2);
            }

            [Test]
            public void ItKnowsTheMaximum()
            {
                _rangeValue.Maximum = 7.2;
                _provider.Maximum.Should().Equal(7.2);
            }

            [Test]
            public void ItKnowsLargeChangeIncrements()
            {
                _rangeValue.LargeChange = 2.0;
                _provider.LargeChange.Should().Equal(2.0);
            }

            [Test]
            public void ItKnowsSmallChangeIncrements()
            {
                _rangeValue.SmallChange = 1.0;
                _provider.SmallChange.Should().Equal(1.0);
            }
        }

        class RangeValueControlStub : RangeValueControl
        {
            public Control Control { get; private set; }

            public RangeValueControlStub()
            {
                Control = new Control();
            }

            public void SetValue(double value)
            {
                Value = value;
            }

            public double Value { get; set; }
            public bool IsReadOnly { get; set; }
            public double Maximum { get; set; }
            public double Minimum { get; set; }
            public double LargeChange { get; set; }
            public double SmallChange { get; set; }
        }
    }
}
