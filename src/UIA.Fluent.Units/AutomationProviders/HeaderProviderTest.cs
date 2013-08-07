using System.Windows.Automation;
using Moq;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Fluent.AutomationProviders
{
    [TestFixture]
    public class HeaderProviderTest
    {
        [Test]
        public void ItHasTheCorrectControlType()
        {
            var table = new Mock<AutomationProvider>();
            new HeaderProvider(table.Object).GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.Header.Id);
        }
    }
}
