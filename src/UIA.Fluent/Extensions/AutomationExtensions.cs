using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIA.Fluent.AutomationProviders;
using UIA.Fluent.AutomationProviders.Tables;

namespace UIA.Fluent.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Before<T>(this IList<T> items, T what)
        {
            var indexOfWhat = items.IndexOf(what);
            return items.ElementAtOrDefault(indexOfWhat - 1);
        }

        public static T After<T>(this IList<T> items, T what)
        {
            var indexOfWhat = items.IndexOf(what);
            return items.ElementAtOrDefault(indexOfWhat + 1);
        }

        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> trueCondition)
        {
            var foundIndex = 0;
            foreach (var item in items)
            {
                if (trueCondition(item)) return foundIndex;
                ++foundIndex;
            }

            return -1;
        }
    }

    public static class AutomationExtensions
    {
        public static AutomationConfigurer ExposeAutomation(this Control control)
        {
            return new AutomationConfigurer(control);
        }

        public static AutomationConfigurer AsValueControl(this Control control, Func<string> getter, Action<string> setter)
        {
            return new AutomationConfigurer(control, new ValueProvider(control, getter, setter));
        }

        public static AutomationConfigurer AsTable(this DataGridView dataGridView)
        {
            return new AutomationConfigurer(dataGridView, new TableProvider(new DataGridTableInformation(dataGridView)));
        }
    }
}
