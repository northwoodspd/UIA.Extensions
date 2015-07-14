using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FizzWare.NBuilder;
using UIA.Extensions.TestApplication.Implementations;

namespace UIA.Extensions.TestApplication
{
    public partial class MainForm : Form
    {
        private readonly BindingSource _bindingSource;

        private readonly string[] _listOptions = { "First Option", "Second Option", "Third Option", };

        public MainForm()
        {
            InitializeComponent();
            basicPanel.ExposeAutomation()
                .WithName("Custom Panel Name");

            monthCalendar.AsValueControl<ValueMonthCalendar>();
            numericUpDown.AsRangeValue();
            dataGridView.AsTable();

            var people = new List<Person>();
            _bindingSource = new BindingSource { DataSource = people };
            dataGridView.DataSource = _bindingSource;
            dataGridView.Columns["SuperSecret"].Visible = false;

            _bindingSource.ListChanged += _bindingSource_ListChanged;

            pictureBox1.AsInvoke(() => toolStripStatusLabel1.Text = "Foos have been pitied!")
                .WithChildren("First Child".TextProvider(), "Second Child".TextProvider());

            fakeCombo.Text = _listOptions.First();
            fakeCombo.AsList<ListActingLabel>();
        }

        void _bindingSource_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            deleteButton.Enabled = _bindingSource.Count != 0;
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string SuperSecret { get; set; }
        }

        private void addRowButton_Click(object sender, EventArgs e)
        {
            var people = Builder<Person>.CreateListOfSize(int.Parse(howManyToAdd.Text)).Build();
            foreach (var person in people)
                _bindingSource.Add(person);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            _bindingSource.RemoveAt(_bindingSource.Count - 1);
        }

        private void updateHeaders_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.HeaderText += " Updated";
            }
        }

        private void toggleRowButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.RowCount > 0)
            {
                _bindingSource.SuspendBinding();
                dataGridView.Rows[0].Visible = !dataGridView.Rows[0].Visible;
                _bindingSource.ResumeBinding();
            }
        }
    }
}
