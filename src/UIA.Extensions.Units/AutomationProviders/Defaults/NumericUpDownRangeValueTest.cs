﻿using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders.Defaults
{
    [TestFixture]
    class NumericUpDownRangeValueTest
    {
        private NumericUpDown _spinner;
        private NumericUpDownRangeValue _numericUpDownProvider;

        [SetUp]
        public void SetUp()
        {
            _spinner = new NumericUpDown { Maximum = decimal.MaxValue };
            _numericUpDownProvider = new NumericUpDownRangeValue(_spinner);
        }

        [Test]
        public void ItCanGetTheValue()
        {
            _spinner.Value = 123.0m;
            _numericUpDownProvider.Value.Should().Be(123.0);
        }

        [Test]
        public void ItCanSetTheValue()
        {
            _numericUpDownProvider.Value = 10.0;
            _spinner.Value.Should().Be(10.0m);
        }

        [Test]
        public void ItHonorsTheReadOnlyStatus()
        {
            _spinner.ReadOnly = true;
            _numericUpDownProvider.IsReadOnly.Should().BeTrue();
        }

        [Test]
        public void ItKnowsTheMinimum()
        {
            _spinner.Minimum = -1.0m;
            _numericUpDownProvider.Minimum.Should().Be(-1.0);
        }

        [Test]
        public void ItKnowsTheMaxiumum()
        {
            _spinner.Maximum = 100.0m;
            _numericUpDownProvider.Maximum.Should().Be(100.0);
        }

        [Test]
        public void SmallChangesAreTheIncrement()
        {
            _spinner.Increment = 2.0m;
            _numericUpDownProvider.SmallChange.Should().Be(2.0);
        }
    }
}
