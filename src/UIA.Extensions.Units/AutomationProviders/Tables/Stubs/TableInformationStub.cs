using System.Collections.Generic;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.Extensions;

namespace UIA.Extensions.AutomationProviders.Tables.Stubs
{
    public class TableInformationStub : TableInformation
    {
        private readonly List<string> _headers;
        private readonly List<RowInformation> _rows;

        public TableInformationStub()
        {
            _headers = new List<string>();
            _rows = new List<RowInformation>();
        }

        public int RowCount { get { return _rows.Count; } }
        public int ColumnCount { get { return _headers.Count; } }

        public Control Control
        {
            get { return new Control(); }
        }

        public List<string> Headers
        {
            get { return _headers; }
        }

        public List<RowInformation> Rows { get { return _rows; } }

        public void AddHeaders(params string[] headers)
        {
            _headers.AddRange(headers);
        }

        public void AddRows(int howMany)
        {
            howMany.Times(x => _rows.Add(new RowInformationStub(x)));
        }
    }
}