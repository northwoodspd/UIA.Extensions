using System.Collections.Generic;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public abstract class TableInformation
    {
        public abstract int RowCount { get; }
        public abstract int ColumnCount { get; }
        public abstract Control Control { get; }
        public abstract List<HeaderInformation> Headers { get; }
        public abstract List<RowInformation> Rows { get; }
        public abstract bool CanSelectMultiple { get; }
    }
}