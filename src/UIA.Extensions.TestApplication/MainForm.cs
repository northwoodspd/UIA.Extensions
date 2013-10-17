using System;
using System.Collections.Generic;
using System.Windows.Automation.Provider;
using System.Windows.Forms;
using FizzWare.NBuilder;
using UIA.Extensions.AutomationProviders;
using UIA.Extensions.TestApplication.Implementations;

namespace UIA.Extensions.TestApplication
{
    public partial class MainForm : Form
    {
        private readonly BindingSource _bindingSource;

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

            _bindingSource.ListChanged += _bindingSource_ListChanged;

            pictureBox1.AsInvoke(() => toolStripStatusLabel1.Text = "Foos have been pitied!")
                .WithChildren("First Child".TextProvider(), "Second Child".TextProvider());
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
    }

    static class Extensions
    {
        public static TextProvider TextProvider(this string value)
        {
            return new TextProvider { Value = value };
        }
    }

    internal class TextProvider : AutomationProvider, IValueProvider
    {
        public override string Name
        {
            get { return Value; }
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
        public bool IsReadOnly { get; private set; }
    }
}
