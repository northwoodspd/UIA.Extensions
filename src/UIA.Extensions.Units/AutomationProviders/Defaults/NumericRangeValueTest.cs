using System.Windows.Forms;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Extensions.AutomationProviders.Defaults
{
    [TestFixture]
    class NumericRangeValueTest
    {
        private NumericUpDown _spinner;
        private NumericRangeValue _numericProvider;

        [SetUp]
        public void SetUp()
        {
            _spinner = new NumericUpDown {Maximum = decimal.MaxValue};
            _numericProvider = new NumericRangeValue(_spinner);
        }

        [Test]
        public void ItCanGetTheValue()
        {
            _spinner.Value = 123.0m;
            _numericProvider.Value.Should().Equal(123.0);
        }

        [Test]
        public void ItCanSetTheValue()
        {
            _numericProvider.Value = 10.0;
            _spinner.Value.Should().Equal(10.0m);
        }

        [Test]
        public void ItHonorsTheReadOnlyStatus()
        {
            _spinner.ReadOnly = true;
            _numericProvider.IsReadOnly.Should().Be.True();
        }

        [Test]
        public void ItKnowsTheMinimum()
        {
            _spinner.Minimum = -1.0m;
            _numericProvider.Minimum.Should().Equal(-1.0);
        }

        [Test]
        public void ItKnowsTheMaxiumum()
        {
            _spinner.Maximum = 100.0m;
            _numericProvider.Maximum.Should().Equal(100.0);
        }

        [Test]
        public void WhoKnowsWhatLargeChangesAre()
        {
            // ??????
        }

        [Test]
        public void SmallChangesAreTheIncrement()
        {
            _spinner.Increment = 2.0m;
            _numericProvider.SmallChange.Should().Equal(2.0);
        }
    }
}
