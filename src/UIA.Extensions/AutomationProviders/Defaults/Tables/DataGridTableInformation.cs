using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
{
    public class DataGridTableInformation : TableInformation
    {
        private readonly DataGridView _dataGrid;

        public DataGridTableInformation(DataGridView dataGrid)
        {
            _dataGrid = dataGrid;
        }

        public override Control Control
        {
            get { return _dataGrid; }
        }

        public override List<string> Headers
        {
            get { return _dataGrid.Columns.Select(x => x.HeaderText).ToList(); }
        }

        public override List<RowInformation> Rows
        {
            get { return _dataGrid.Rows.Select(DataGridRowInformation.FromRow).ToList(); }
        }

        public override bool CanSelectMultiple
        {
            get { return _dataGrid.MultiSelect; }
        }

        public override int RowCount
        {
            get { return _dataGrid.RowCount; }
        }

        public override int ColumnCount
        {
            get { return _dataGrid.ColumnCount; }
        }
    }
}