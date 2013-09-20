using System;
using System.Windows;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables.Stubs
{
    public class CellInformationStub : CellInformation
    {
        private string _value;
        private int _row;
        private int _column;
        private Rect _location;

        public CellInformationStub(string value)
        {
            _value = value;
        }

        public CellInformationStub()
        {
            _value = String.Empty;
        }

        public override string Value
        {
            get { return _value; }
        }

        public override int Row
        {
            get { return _row; }
        }

        public override int Column
        {
            get { return _column; }
        }

        public override Rect Location
        {
            get { return _location; }
        }

        public string ExpectedValue { set { _value = value; } }
        public int ExpectedRow { set { _row = value; } }
        public int ExpectedColumn { set { _column = value; } }
        public Rect ExpectedLocation { set { _location = value; } }
    }
}