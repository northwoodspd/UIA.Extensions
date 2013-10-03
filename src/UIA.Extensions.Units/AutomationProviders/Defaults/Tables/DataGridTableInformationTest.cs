using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
{
    [TestFixture]
    class DataGridTableInformationTest
    {
        private DataGridView _dataGridView;
        private DataGridTableInformation _tableInformation;

        [SetUp]
        public void SetUp()
        {
            _dataGridView = new DataGridView();
            _tableInformation = new DataGridTableInformation(_dataGridView);
        }

        [Test]
        public void ItKnowsIfMultipleSelectionsAreReported()
        {
            _dataGridView.MultiSelect = true;
            _tableInformation.CanSelectMultiple.Should().BeTrue();
        }
    }
}
