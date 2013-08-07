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
    public class AutomationControlProviderTest
    {
        private TestAutomationControlProvider _controlProvider;
        private Control _control;

        [SetUp]
        public void SetUp()
        {
            _control = new Control {Name = "expectedControlAutomationId"};
            _controlProvider = new TestAutomationControlProvider(_control);
        }

        [Test]
        public void ItDefaultsAsACustomControl()
        {
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                     .Should().Equal(ControlType.Custom.Id);
        }

        [Test]
        public void ItUsesTheControlsTypeForTheAutomationControlType()
        {
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
                     .Should().Equal(typeof(Control).FullName);
        }

        [Test]
        public void ItUsesTheControlNameForTheAutomationId()
        {
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                     .Should().Equal("expectedControlAutomationId");
        }

        [Test]
        public void ItIsConsideredAServerSideProvider()
        {
            (_controlProvider.ProviderOptions & ProviderOptions.ServerSideProvider)
                .Should()
                .Equal(ProviderOptions.ServerSideProvider);
        }

        [Test]
        public void ItUsesComThreading()
        {
            ((int)_controlProvider.ProviderOptions & AutomationControlProvider.ProviderUseComThreading)
                .Should()
                .Equal(AutomationControlProvider.ProviderUseComThreading);
        }

        [Test]
        public void ItShouldAllowAdditionalPropertiesToBeSet()
        {
            _controlProvider.SetPropertyValue(AutomationElementIdentifiers.ClassNameProperty.Id, "ExpectedPropertyValue");
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.ClassNameProperty.Id)
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
            _controlProvider.Set(whichWay, expectedSurrounding.Object);
            _controlProvider.Navigate(whichWay)
                     .Should().Be.SameAs(expectedSurrounding.Object);
        }

        private Mock<AutomationControlProvider> GetMockChild()
        {
            return new Mock<AutomationControlProvider>(new Control());
        }

        public class TestAutomationControlProvider : AutomationControlProvider
        {
            private readonly Dictionary<NavigateDirection, IRawElementProviderFragment> _surroundings;

            public TestAutomationControlProvider(Control control) : base(control)
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

            public void Set(NavigateDirection whichWay, AutomationControlProvider automationControlProvider)
            {
                _surroundings[whichWay] = automationControlProvider;
            }
        }
    }

}
