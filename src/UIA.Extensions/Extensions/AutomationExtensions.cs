using System;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders;
using UIA.Extensions.AutomationProviders.Defaults.Tables;
using UIA.Extensions.AutomationProviders.Tables;

namespace UIA.Extensions.Extensions
{
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
