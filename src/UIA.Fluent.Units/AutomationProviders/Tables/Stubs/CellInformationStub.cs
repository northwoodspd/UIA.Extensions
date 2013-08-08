using System;

namespace UIA.Fluent.AutomationProviders.Tables.Stubs
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
    }
}