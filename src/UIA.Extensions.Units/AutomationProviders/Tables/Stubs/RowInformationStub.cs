using System.Collections.Generic;
using NUnit.Framework;
using UIA.Extensions.AutomationProviders.Interfaces.Tables;

namespace UIA.Extensions.AutomationProviders.Tables.Stubs
{
    public class RowInformationStub : RowInformation
    {
        private bool _wasSelected;
        private string _value;
        private List<CellInformation> _cells;

        public RowInformationStub()
            : this("Default")
        { }

        public RowInformationStub(int which)
            : this("Row" + which)
        { }

        public RowInformationStub(string what)
        {
            _cells = new List<CellInformation>();
            _value = what;
        }

        public RowInformationStub(List<CellInformation> cells)
        {
            _cells = cells;
        }

        public override string Value
        {
            get { return _value; }
        }

        public override List<CellInformation> Cells
        {
            get { return _cells; }
        }

        public override void Select()
        {
            _wasSelected = true;
        }

        public override bool IsSelected
        {
            get { return _wasSelected; }
        }

        public void ShouldHaveBeenSelected()
        {
            Assert.That(_wasSelected, Is.True, "Expected Select to have been called but it was not");
        }
    }
}