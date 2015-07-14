namespace UIA.Extensions.AutomationProviders.Interfaces
{
    public class ListItemInformation
    {
        public ListItemInformation(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}