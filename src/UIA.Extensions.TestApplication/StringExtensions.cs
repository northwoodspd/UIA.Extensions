using UIA.Extensions.TestApplication.Implementations;

namespace UIA.Extensions.TestApplication
{
    static class StringExtensions
    {
        public static TextProvider TextProvider(this string value)
        {
            return new TextProvider { Value = value };
        }
    }
}