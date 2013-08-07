namespace UIA.Fluent.AutomationProviders.Tables
{
    public class TableValueProvider : ChildProvider
    {
        public TableValueProvider(AutomationProvider parent, CellInformation cell) : base(parent)
        {
            Name = cell.Value;
        }
    }
}