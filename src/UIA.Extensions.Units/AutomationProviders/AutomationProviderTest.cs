using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders
{
    [TestFixture]
    public class AutomationProviderTest
    {
        private TestAutomationProvider _automationProvider;

        [SetUp]
        public void SetUp()
        {
            _automationProvider = new TestAutomationProvider();
        }

        [Test]
        public void ItDefaultsAsACustomControl()
        {
            _automationProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .ShouldBeEquivalentTo(ControlType.Custom.Id);
        }

        [Test]
        public void ControlTypesCanBeOverridden()
        {
            _automationProvider.ControlType = ControlType.Spinner;
            _automationProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .ShouldBeEquivalentTo(ControlType.Spinner.Id);
        }

        [Test]
        public void TheLocalizedControlTypeIsReflected()
        {
            _automationProvider.ControlType = ControlType.Spinner;

            _automationProvider.GetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
                     .ShouldBeEquivalentTo("spinner");
        }

        [Test]
        public void ItReflectsTheName()
        {
            _automationProvider.Name = "whatever";
            _automationProvider.GetPropertyValue(AutomationElementIdentifiers.NameProperty.Id)
                .ShouldBeEquivalentTo("whatever");
        }

        [Test]
        public void TheIdCanBeOverridden()
        {
            _automationProvider.Id = "expectedAutomationId";
            _automationProvider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                .ShouldBeEquivalentTo("expectedAutomationId");
        }

        [Test]
        public void ItIsConsideredAServerSideProvider()
        {
            (_automationProvider.ProviderOptions & ProviderOptions.ServerSideProvider)
                .ShouldBeEquivalentTo(ProviderOptions.ServerSideProvider);
        }

        [Test]
        public void ItUsesComThreading()
        {
            ((int)_automationProvider.ProviderOptions & AutomationProvider.ProviderUseComThreading)
                .ShouldBeEquivalentTo(AutomationProvider.ProviderUseComThreading);
        }

        [Test]
        public void ItShouldAllowAdditionalPropertiesToBeSet()
        {
            _automationProvider.SetPropertyValue(AutomationElementIdentifiers.ClassNameProperty.Id, "ExpectedPropertyValue");
            _automationProvider.GetPropertyValue(AutomationElementIdentifiers.ClassNameProperty.Id)
                .ShouldBeEquivalentTo("ExpectedPropertyValue");
        }

        [TestCase(NavigateDirection.FirstChild)]
        [TestCase(NavigateDirection.LastChild)]
        [TestCase(NavigateDirection.NextSibling)]
        [TestCase(NavigateDirection.PreviousSibling)]
        [TestCase(NavigateDirection.Parent)]
        public void ItIsAwareOfItsSurroundings(NavigateDirection whichWay)
        {
            var expectedSurrounding = GetMockChild();
            _automationProvider.Set(whichWay, expectedSurrounding.Object);
            _automationProvider.Navigate(whichWay)
                .Should().BeSameAs(expectedSurrounding.Object);
        }

        [TestFixture]
        public class ChildProviders
        {
            private Mock<AutomationProvider> _parent;
            private AutomationProvider _childProvider;

            [SetUp]
            public void SetUp()
            {
                _parent = new Mock<AutomationProvider>();
                _childProvider = new AutomationProvider(_parent.Object);
            }

            [Test]
            public void HaveAParent()
            {
                _childProvider.Navigate(NavigateDirection.Parent)
                    .Should().BeSameAs(_parent.Object);
            }

            [Test]
            public void ItNeverForgetsItsRoots()
            {
                var expectedRoot = new Mock<AutomationProvider>();
                _parent.Setup(x => x.FragmentRoot).Returns(expectedRoot.Object);

                _childProvider.FragmentRoot.Should().BeSameAs(expectedRoot.Object);
            }

            [TestFixture]
            public class Navigation
            {
                private Mock<AutomationProvider> _parent;
                private readonly List<AutomationProvider> _chillins = new List<AutomationProvider>();

                [SetUp]
                public void SetUp()
                {
                    _parent = new Mock<AutomationProvider>();
                    _parent.Setup(x => x.Children).Returns(_chillins);
                }

                [TearDown]
                public void TearDown()
                {
                    _chillins.Clear();
                }

                [Test]
                public void OneChildHasNoNextOrPrevious()
                {
                    var child = AddChild();
                    Before(child).Should().BeNull();
                    After(child).Should().BeNull();
                }

                [Test]
                public void KnowsAboutTheNextChild()
                {
                    var first = AddChild();
                    var second = AddChild();
                    After(first).Should().BeSameAs(second);
                }

                [Test]
                public void KnowsAboutThePreviousChild()
                {
                    var first = AddChild();
                    var second = AddChild();
                    Before(second).Should().BeSameAs(first);
                }

                private AutomationProvider AddChild()
                {
                    _chillins.Add(new AutomationProvider(_parent.Object));
                    return _chillins.Last();
                }

                private static IRawElementProviderFragment Before(IRawElementProviderFragment child)
                {
                    return child.Navigate(NavigateDirection.PreviousSibling);
                }

                private static IRawElementProviderFragment After(IRawElementProviderFragment child)
                {
                    return child.Navigate(NavigateDirection.NextSibling);
                }
            }
        }


        private static Mock<ControlProvider> GetMockChild()
        {
            return new Mock<ControlProvider>(new Control());
        }

        public class TestAutomationProvider : AutomationProvider
        {
            private readonly Dictionary<NavigateDirection, AutomationProvider> _surroundings;

            public TestAutomationProvider()
            {
                _surroundings = new Dictionary<NavigateDirection, AutomationProvider>();
            }

            protected override IRawElementProviderFragment FirstChild
            {
                get { return _surroundings[NavigateDirection.FirstChild]; }
            }

            protected override IRawElementProviderFragment LastChild
            {
                get { return _surroundings[NavigateDirection.LastChild]; }
            }

            protected override IRawElementProviderFragment NextSibling
            {
                get { return _surroundings[NavigateDirection.NextSibling]; }
            }

            protected override IRawElementProviderFragment PreviousSibling
            {
                get { return _surroundings[NavigateDirection.PreviousSibling]; }
            }

            public override AutomationProvider Parent
            {
                get { return _surroundings[NavigateDirection.Parent]; }
            }

            public void Set(NavigateDirection whichWay, ControlProvider controlProvider)
            {
                _surroundings[whichWay] = controlProvider;
            }
        }
    }
}