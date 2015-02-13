using System.Collections.Generic;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;
using UIA.Extensions.InternalExtensions;

namespace UIA.Extensions.AutomationProviders.Tables.Stubs
{
    public class TableInformationStub : TableInformation
    {
        private readonly List<HeaderInformation> _headers;
        private readonly List<RowInformation> _rows;

        public TableInformationStub()
        {
            _headers = new List<HeaderInformation>();
            _rows = new List<RowInformation>();
        }

        public override int RowCount { get { return _rows.Count; } }
        public override int ColumnCount { get { return _headers.Count; } }

        public override Control Control
        {
            get { return new Control(); }
        }

        public override List<HeaderInformation> Headers
        {
            get { return _headers; }
        }

        public override List<RowInformation> Rows { get { return _rows; } }

        public bool OverriddenCanSelectMultiple { get; set; }
        public override bool CanSelectMultiple { get { return OverriddenCanSelectMultiple; } }

        public void AddHeaders(params HeaderInformation[] headers)
        {
            _headers.AddRange(headers);
        }

        public void AddRows(int howMany)
        {
            howMany.Times(x => _rows.Add(new RowInformationStub(x)));
        }
    }
}