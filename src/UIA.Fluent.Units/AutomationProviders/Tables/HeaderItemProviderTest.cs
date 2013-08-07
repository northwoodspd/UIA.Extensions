using System.Windows.Automation;
using NUnit.Framework;
using Should.Fluent;

namespace UIA.Fluent.AutomationProviders.Tables
{
    [TestFixture]
    public class HeaderItemProviderTest
    {
        [Test]
        public void ItHasTheHeaderItemType()
        {
            new HeaderItemProvider(null, null).GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.HeaderItem.Id);
        }
    }
}
