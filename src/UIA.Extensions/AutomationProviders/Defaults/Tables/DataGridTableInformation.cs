using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Defaults.Tables
{
    public class DataGridTableInformation : TableInformation
    {
        protected readonly DataGridView DataGrid;

        public DataGridTableInformation(DataGridView dataGrid)
        {
            DataGrid = dataGrid;
        }

        public override Control Control
        {
            get { return DataGrid; }
        }

        public override List<string> Headers
        {
            get { return DataGrid.Columns.Where(x => x.Visible).Select(HeaderTextOrColumnName).ToList(); }
        }

        public override List<RowInformation> Rows
        {
            get { return DataGrid.Rows.Select(DataGridRowInformation.FromRow).ToList(); }
        }

        public override bool CanSelectMultiple
        {
            get { return DataGrid.MultiSelect; }
        }

        public override int RowCount
        {
            get { return DataGrid.RowCount; }
        }

        public override int ColumnCount
        {
            get { return DataGrid.ColumnCount; }
        }

        private static string HeaderTextOrColumnName(DataGridViewColumn column)
        {
            return string.IsNullOrEmpty(column.HeaderText) ? column.Name : column.HeaderText;
        }
    }
}