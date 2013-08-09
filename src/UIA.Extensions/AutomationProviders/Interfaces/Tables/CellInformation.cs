using System.Windows;

namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public interface CellInformation
    {
        string Value { get; }
        int Row { get;  }
        int Column { get;  }
        Rect Location { get; }
    }
}