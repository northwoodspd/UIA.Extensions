using System.Windows.Automation;
using System.Windows.Forms;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Extensions.AutomationProviders
{
    [TestFixture]
    public class ControlProviderTest
    {
        private ControlProvider _controlProvider;

        [SetUp]
        public void SetUp()
        {
            _controlProvider = new ControlProvider(new Control {Name = "expectedControlAutomationId"});
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
        public void ItProperlyReflectsAChangedNameInTheAutomationId()
        {
            var originalName = "expectedControlAutomationId";
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                     .Should().Equal(originalName);

            _controlProvider.Control.Name = "modifiedAutomationId";
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                     .Should().Equal("modifiedAutomationId");
        }

        public class TestControlProvider : ControlProvider
        {
            public TestControlProvider(Control control) : base(control)
            { }
        }
    }
}
