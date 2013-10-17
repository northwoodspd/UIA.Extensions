using System;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders;
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

        [TestFixture]
        public class Children
        {
            private Control _control;

            [SetUp]
            public void SetUp()
            {
                _control = new Control();
            }

            [Test]
            public void ItCanAddChildrenProviders()
            {
                var childProvider = new SomeGenericProvider();

                var parentProvider = _control.AsValueControl<TestValueControl>()
                        .WithChildren(childProvider).ControlProvider;

                childProvider.Parent.Should().BeSameAs(parentProvider);
                parentProvider.Children.Should().Equal(new [] { childProvider });
            }

            [Test]
            public void ItCanAddMultipleChildrenProviders()
            {
                var firstChild = new SomeGenericProvider();
                var secondChild = new SomeGenericProvider();

                var parentProvider = _control.AsValueControl<TestValueControl>()
                       .WithChildren(firstChild, secondChild).ControlProvider;

                parentProvider.Children.Should().Equal(new[] {firstChild, secondChild});
                parentProvider.Children.TrueForAll(child => child.Parent == parentProvider);
            }
        }
    }

    internal class TestInvokeControl : InvokeControl
    {
        public TestInvokeControl(Control control)
            : base(control)
        { }

        public override void Invoke()
        { }
    }

    internal class TestValueControl : ValueControl
    {
        public TestValueControl(Control control)
            : base(control)
        {
        }

        public override string Value { get; set; }
    }

    internal class SomeGenericProvider : AutomationProvider
    {
    }
}
