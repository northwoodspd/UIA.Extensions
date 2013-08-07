using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FizzWare.NBuilder;
using UIA.Fluent.Extensions;

namespace UIA.Fluent.TestApplication
{
    public partial class MainForm : Form
    {
        private readonly BindingSource _bindingSource;

        public MainForm()
        {
            InitializeComponent();
            basicPanel.ExposeAutomation()
                .WithName("Custom Panel Name");

            monthCalendar.AsValueControl(() => monthCalendar.SelectionStart.ToShortDateString(),
                x => monthCalendar.SetDate(DateTime.Parse(x)));

            dataGridView.AsTable();

            var people = new List<Person>();
            _bindingSource = new BindingSource {DataSource = people};
            dataGridView.DataSource = _bindingSource;
            dataGridView.ReadOnly = true;
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }

        private void addRowButton_Click(object sender, EventArgs e)
        {
            _bindingSource.Add(Builder<Person>.CreateNew().Build());
        }
    }
}
