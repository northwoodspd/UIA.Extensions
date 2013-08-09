using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace UIA.Extensions.AutomationProviders.Tables
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
        List<CellInformation> Cells { get; }
        void Select();
        bool IsSelected { get; }
    }

    public interface CellInformation
    {
        string Value { get; }
        int Row { get;  }
        int Column { get;  }
        Rect Location { get; }
    }
}