using System.Collections.Generic;
using System.Windows.Forms;

namespace UIA.Fluent.AutomationProviders.Tables
{
    public interface TableInformation
    {
        int RowCount { get; }
        Control Control { get; }
        IList<string> Headers { get; }
    }
}