using System.Windows.Automation;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;

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
        public void ItUsesTheControlNameForTheAutomationId()
        {
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                     .ShouldBeEquivalentTo("expectedControlAutomationId");
        }

        [Test]
        public void ItUsesTheControlTextForTheAutomationName()
        {
            _controlProvider.Control.Text = "The Control Text";

            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.NameProperty.Id)
                     .ShouldBeEquivalentTo("The Control Text");
        }

        [Test]
        public void ItProperlyReflectsAChangedNameInTheAutomationId()
        {
            var originalName = "expectedControlAutomationId";
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                     .ShouldBeEquivalentTo(originalName);

            _controlProvider.Control.Name = "modifiedAutomationId";
            _controlProvider.GetPropertyValue(AutomationElementIdentifiers.AutomationIdProperty.Id)
                     .ShouldBeEquivalentTo("modifiedAutomationId");
        }

        public class TestControlProvider : ControlProvider
        {
            public TestControlProvider(Control control) : base(control)
            { }
        }
    }
}
