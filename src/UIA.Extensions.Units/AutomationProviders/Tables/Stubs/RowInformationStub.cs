using System.Collections.Generic;
using NUnit.Framework;

namespace UIA.Extensions.AutomationProviders.Tables.Stubs
{
    public class RowInformationStub : RowInformation
    {
        private bool _wasSelected;

        public RowInformationStub()
            : this("Default")
        { }

        public RowInformationStub(int which)
            : this("Row" + which)
        { }

        public RowInformationStub(string what)
        {
            Cells = new List<CellInformation>();
            Value = what;
        }

        public string Value { get; private set; }
        public List<CellInformation> Cells { get; set; }

        public void Select()
        {
            _wasSelected = true;
        }

        public bool IsSelected
        {
            get
            {
                return _wasSelected;
            }
        }

        public void ShouldHaveBeenSelected()
        {
            Assert.That(_wasSelected, Is.True, "Expected Select to have been called but it was not");
        }
    }
}