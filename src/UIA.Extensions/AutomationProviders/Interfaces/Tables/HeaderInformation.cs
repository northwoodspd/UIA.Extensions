namespace UIA.Extensions.AutomationProviders.Interfaces.Tables
{
    public class HeaderInformation
    {
        public HeaderInformation()
        { }

        public HeaderInformation(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
        public bool IsVisible { get; set; }

        public static HeaderInformation NullHeader
        {
            get {  return new HeaderInformation(); }
        }
    }
}