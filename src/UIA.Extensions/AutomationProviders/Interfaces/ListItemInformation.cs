namespace UIA.Extensions.AutomationProviders.Interfaces
{
    public abstract class ListItemInformation
    {
        protected ListItemInformation(string text)
        {
            Text = text;
        }

        public string Text { get; protected set; }
        public bool IsSelected { get; protected set; }
        public abstract void Select();
        public abstract void AddToSelection();
        public abstract void RemoveFromSelection();
    }
}