using System.Collections.Generic;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public interface TableInformation
    {
        int RowCount { get; }
        int ColumnCount { get; }
        Control Control { get; }
        List<string> Headers { get; }
        List<RowInformation> Rows { get; }
    }
}