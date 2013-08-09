using System.Collections.Generic;

namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public interface RowInformation
    {
        string Value { get; }
        List<CellInformation> Cells { get; }
        void Select();
        bool IsSelected { get; }
    }
}