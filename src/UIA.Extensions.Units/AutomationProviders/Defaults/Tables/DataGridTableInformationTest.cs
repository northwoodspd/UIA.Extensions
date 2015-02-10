using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using UIA.Extensions.InternalExtensions;

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
        [Test]
        public void ItKnowsAboutTheHeaders()
        {
            AddHeaders("First", "Second");
            _tableInformation.Headers.Should().Equal(new[] { "First", "Second" });
        }
        [Test]
        public void HeadersUseTheColumnNameIfThereIsNoDisplayName()
        {
            AddHeader("headerName", "Display Name");
            AddHeader("otherName", "");
            _tableInformation.Headers.Should().Equal(new[] { "Display Name", "otherName" });
        }
        [Test]
        public void HiddenHeadersAreNotIncluded()
        {
            AddHeader("Visible Column", string.Empty);
            AddHeader("Invisible Column", string.Empty, false);

            _tableInformation.Headers.Should().Equal(new[] { "Visible Column" });
        }

        private void AddHeader(string columnName, string displayName, bool visible = true)
        {
            int index = _dataGridView.Columns.Add(columnName, displayName);
            _dataGridView.Columns[index].Visible = visible;
        }

        private void AddHeaders(params string[] headers)
        {
            headers.ForEach(x => AddHeader(x, x));
        }
    }
}
