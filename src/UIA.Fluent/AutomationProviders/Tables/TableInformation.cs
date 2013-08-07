using System.Collections.Generic;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders.Tables
{
    public interface TableInformation
    {
        int RowCount { get; }
        int ColumnCount { get; }
        Control Control { get; }
        List<string> Headers { get; }
        List<RowInformation> Rows { get; }
    }

    public interface RowInformation
    {
        string Value { get; }
        List<string> Values { get; }
    }
}