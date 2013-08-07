namespace UIA.Fluent.AutomationProviders.Tables
{
    public class TableCellProvider : ChildProvider
    {
        public TableCellProvider(AutomationProvider parent, CellInformation cell) : base(parent)
        {
            Name = cell.Value;
        }
    }
}