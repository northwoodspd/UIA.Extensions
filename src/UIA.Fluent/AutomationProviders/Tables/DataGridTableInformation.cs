using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders.Tables
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

        public IList<string> Headers
        {
            get { return (from DataGridViewColumn column in _dataGrid.Columns select column.HeaderText).ToList(); }
        }

        public int RowCount
        {
            get { return _dataGrid.RowCount; }
        }
    }
}