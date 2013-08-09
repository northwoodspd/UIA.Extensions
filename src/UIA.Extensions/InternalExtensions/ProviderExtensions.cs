using System.Windows.Automation.Provider;

namespace UIA.Extensions.InternalExtensions
{
    static class ProviderExtensions
    {
        public static IRawElementProviderFragment Parent(this IRawElementProviderFragment element)
        {
            return element.Navigate(NavigateDirection.Parent);
        }
        public static IRawElementProviderFragment FirstChild(this IRawElementProviderFragment element)
        {
            return element.Navigate(NavigateDirection.FirstChild);
        }

        public static IRawElementProviderFragment LastChild(this IRawElementProviderFragment element)
        {
            return element.Navigate(NavigateDirection.LastChild);
        }

        public static IRawElementProviderFragment PreviousSibling(this IRawElementProviderFragment element)
        {
            return element.Navigate(NavigateDirection.NextSibling);
        }

        public static IRawElementProviderFragment NextSibling(this IRawElementProviderFragment element)
        {
            return element.Navigate(NavigateDirection.NextSibling);
        }
    }
}