using System.Windows.Automation;
using System.Windows.Forms;
using Moq;
using NUnit.Framework;
using Should.Fluent;
using UIA.Fluent.AutomationProviders.Tables;

namespace UIA.Fluent.AutomationProviders
{
    [TestFixture]
    public class TableProviderTest
    {
        private Mock<TableInformation> _tableInformation;
        private TableControlProvider _tableControlProvider;

        [SetUp]
        public void SetUp()
        {
            _tableInformation = new Mock<TableInformation>();
            _tableInformation.Setup(x => x.Control).Returns(new Control());

            _tableControlProvider = new TableControlProvider(_tableInformation.Object);
        }

        [Test]
        public void ItHasTheTableControlType()
        {
            _tableControlProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.Table.Id);
        }

        [Test]
        public void ItIsBothOfTypeGridAndOfTypeTable()
        {
            _tableControlProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_tableControlProvider);

            _tableControlProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_tableControlProvider);
        }

        [Test]
        public void ItReturnsTheNumberOfRows()
        {
            _tableInformation.Setup(x => x.RowCount).Returns(7);

            _tableControlProvider.RowCount.Should().Equal(7);
        }
    }
}
