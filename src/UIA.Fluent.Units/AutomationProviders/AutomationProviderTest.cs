using System;
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using Moq;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Fluent.AutomationProviders
{
    [TestFixture]
    public class AutomationProviderTest
    {
        private TestAutomationProvider _provider;
        private Control _control;

        [SetUp]
        public void SetUp()
        {
            _control = new Control {Name = "expectedControlAutomationId"};
            _provider = new TestAutomationProvider(_control);
        }

        [Test]
        public void ItDefaultsAsACustomControl()
        {
            _provider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                     .Should().Equal(ControlType.Custom.Id);
        }

        [Test]
        public void ItUsesTheControlsTypeForTheAutomationControlType()
        {
            _provider.GetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
                     .Should().Equal(typeof(Control).FullName);
        }

        [Test]
        public void ItUsesTheControlNameForTheAutomationId()
        {
            _provider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                     .Should().Equal("expectedControlAutomationId");
        }

        [Test]
        public void ItIsConsideredAServerSideProvider()
        {
            (_provider.ProviderOptions & ProviderOptions.ServerSideProvider)
                .Should()
                .Equal(ProviderOptions.ServerSideProvider);
        }

        [Test]
        public void ItUsesComThreading()
        {
            ((int)_provider.ProviderOptions & AutomationProvider.ProviderUseComThreading)
                .Should()
                .Equal(AutomationProvider.ProviderUseComThreading);
        }

        [Test]
        public void ItShouldAllowAdditionalPropertiesToBeSet()
        {
            _provider.SetPropertyValue(AutomationElementIdentifiers.ClassNameProperty.Id, "ExpectedPropertyValue");
            _provider.GetPropertyValue(AutomationElementIdentifiers.ClassNameProperty.Id)
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
            _provider.Set(whichWay, expectedSurrounding.Object);
            _provider.Navigate(whichWay)
                     .Should().Be.SameAs(expectedSurrounding.Object);
        }

        private Mock<AutomationProvider> GetMockChild()
        {
            return new Mock<AutomationProvider>(new Control());
        }

        public class TestAutomationProvider : AutomationProvider
        {
            private readonly Dictionary<NavigateDirection, IRawElementProviderFragment> _surroundings;

            public TestAutomationProvider(Control control) : base(control)
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

            public void Set(NavigateDirection whichWay, AutomationProvider automationProvider)
            {
                _surroundings[whichWay] = automationProvider;
            }
        }
    }

}
