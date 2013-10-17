using System;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces;

namespace UIA.Extensions
{
    [TestFixture]
    class AutomationExtensionsTest
    {
        private Control _control;

        [SetUp]
        public void SetUp()
        {
            _control = new Control();
        }

        [Test]
        public void ItCanMakeControlsValuable()
        {
            var provider = _control.AsValueControl<TestValueControl>().ControlProvider;

            provider.GetPatternProvider(ValuePatternIdentifiers.Pattern.Id)
                    .Should().BeSameAs(provider);

            provider.Control.Should().BeSameAs(_control);
        }

        [Test]
        public void ItCanMakeThemInvokableViaInterface()
        {
            var provider = _control.AsInvoke<TestInvokeControl>().ControlProvider;

            provider.GetPatternProvider(InvokePatternIdentifiers.Pattern.Id)
                    .Should().BeSameAs(provider);

            provider.Control.Should().BeSameAs(_control);
        }

        [Test]
        public void ItCanBeInvokableThroughActions()
        {
            var expected = String.Empty;

            var invokable =
                _control.AsInvoke(() => expected = "Expected Value")
                        .ControlProvider.GetPatternProvider(InvokePatternIdentifiers.Pattern.Id) as IInvokeProvider;

            invokable.Invoke();

            expected.Should().Be("Expected Value");
        }
    }

    internal class TestInvokeControl : InvokeControl
    {
        public TestInvokeControl(Control control) : base(control)
        {}

        public override void Invoke()
        {}
    }

    internal class TestValueControl : ValueControl
    {
        public TestValueControl(Control control) : base(control)
        {
        }

        public override string Value { get; set; }
    }
}
