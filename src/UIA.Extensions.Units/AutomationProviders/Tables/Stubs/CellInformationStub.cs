using System;
using System.Windows;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables.Stubs
{
    public class CellInformationStub : CellInformation
    {
        public CellInformationStub(string value)
        {
            Value = value;
        }

        public CellInformationStub()
        {
            Value = String.Empty;
        }

        public string Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Rect Location { get; set; }
    }
}