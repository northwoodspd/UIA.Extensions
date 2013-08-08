using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using Moq;
using NUnit.Framework;
using Should.Fluent;

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
                .Should().Equal(ControlType.Custom.Id);
        }

        [Test]
        public void TheIdCanBeOverridden()
        {
            _automationProvider.Id = "expectedAutomationId";
            _automationProvider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                .Should().Equal("expectedAutomationId");
        }

        [Test]
        public void ItIsConsideredAServerSideProvider()
        {
            (_automationProvider.ProviderOptions & ProviderOptions.ServerSideProvider)
                .Should()
                .Equal(ProviderOptions.ServerSideProvider);
        }

        [Test]
        public void ItUsesComThreading()
        {
            ((int)_automationProvider.ProviderOptions & AutomationProvider.ProviderUseComThreading)
                .Should()
                .Equal(AutomationProvider.ProviderUseComThreading);
        }

        [Test]
        public void ItShouldAllowAdditionalPropertiesToBeSet()
        {
            _automationProvider.SetPropertyValue(AutomationElementIdentifiers.ClassNameProperty.Id, "ExpectedPropertyValue");
            _automationProvider.GetPropertyValue(AutomationElementIdentifiers.ClassNameProperty.Id)
                .Should().Equal("ExpectedPropertyValue");
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
                .Should().Be.SameAs(expectedSurrounding.Object);
        }

        private static Mock<ControlProvider> GetMockChild()
        {
            return new Mock<ControlProvider>(new Control());
        }

        public class TestAutomationProvider : AutomationProvider
        {
            private readonly Dictionary<NavigateDirection, IRawElementProviderFragment> _surroundings;

            public TestAutomationProvider()
            {
                _surroundings = new Dictionary<NavigateDirection, IRawElementProviderFragment>();
            }

            protected override List<int> SupportedPatterns
            {
                get { throw new NotImplementedException(); }
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

            protected override IRawElementProviderFragment Parent
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