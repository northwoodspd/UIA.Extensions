using System.Windows.Forms;

namespace UIA.Fluent.TestApplication
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            basicPanel.ExposeAutomation()
                .WithName("Custom Panel Name");
        }
    }
}
