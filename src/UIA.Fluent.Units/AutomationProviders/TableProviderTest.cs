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
        private TableProvider _tableProvider;

        [SetUp]
        public void SetUp()
        {
            _tableInformation = new Mock<TableInformation>();
            _tableInformation.Setup(x => x.Control).Returns(new Control());

            _tableProvider = new TableProvider(_tableInformation.Object);
        }

        [Test]
        public void ItHasTheTableControlType()
        {
            _tableProvider.GetPropertyValue(AutomationElementIdentifiers.ControlTypeProperty.Id)
                .Should().Equal(ControlType.Table.Id);
        }

        [Test]
        public void ItIsBothOfTypeGridAndOfTypeTable()
        {
            _tableProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_tableProvider);

            _tableProvider.GetPatternProvider(TablePatternIdentifiers.Pattern.Id)
                .Should().Be.SameAs(_tableProvider);
        }

        [Test]
        public void ItReturnsTheNumberOfRows()
        {
            _tableInformation.Setup(x => x.RowCount).Returns(7);

            _tableProvider.RowCount.Should().Equal(7);
        }
    }
}
