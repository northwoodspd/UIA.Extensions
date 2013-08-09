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

        public Control Control
        {
            get { return _dataGrid; }
        }

        public List<string> Headers
        {
            get { return _dataGrid.Columns.Select(x => x.HeaderText).ToList(); }
        }

        public List<RowInformation> Rows
        {
            get { return _dataGrid.Rows.Select(DataGridRowInformation.FromRow).ToList(); }
        }

        public int RowCount
        {
            get { return _dataGrid.RowCount; }
        }

        public int ColumnCount
        {
            get { return _dataGrid.ColumnCount; }
        }
    }
}