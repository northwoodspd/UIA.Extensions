# UIA.Extensions

`UIA.Extensions` is a .NET library that assists in exposing window controls to [Automation](http://msdn.microsoft.com/en-us/library/ms747327.aspx).

## Supported Controls

* [Value Controls](#value-pattern)
* [RangeValue Controls](#spinners)
* [Table Controls](#tables)

### Value Pattern
The [`ValuePattern`](http://msdn.microsoft.com/en-us/library/system.windows.automation.valuepattern.aspx) allows you to get an set the value of a control. Not all controls support this pattern out of the box. Here is an example of how you might use `UIA.Extensions` to expose a [`MonthCalendar`](http://msdn.microsoft.com/en-us/library/system.windows.forms.monthcalendar.aspx) control to automation:


#### `MainForm.cs`

```csharp
using System;
using System.Windows.Forms;
using UIA.Extensions.AutomationProviders;

namespace YourApp
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();

      monthCalendar.AsValueControl<ValueMonthCalendar>();
    }
  }

  public class ValueMonthCalendar : ValueControl
  {
    private readonly MonthCalendar _monthCalendar;

    public ValueMonthCalendar(MonthCalendar monthCalendar) : base(monthCalendar)
    {
      _monthCalendar = monthCalendar;
    }

    public override string Value
    {
      get { return _monthCalendar.SelectionStart.ToShortDateString(); }
      set { _monthCalendar.SetDate(DateTime.Parse(value)); }
    }
  }
}

```

### Spinners
The [`RangeValuePattern`](http://msdn.microsoft.com/en-us/library/system.windows.automation.rangevaluepattern.aspx) is one that is generally used by spinner controls, such as the [`NumericUpDown`](http://msdn.microsoft.com/en-us/library/system.windows.forms.numericupdown.aspx) control. Looking at the default `NumericUpDown` automation information that is expose yields what _looks_ like is something that is usable but in fact is not. Out of the box, the `NumericUpDown` control will not allow you to set its value through automation, but with `UIA.Extensions` you can. There is already a default implementation for this, and it is used like this:

```csharp
using UIA.Extensions;

namespace YourApp
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();

      numericUpDown.AsRangeValue(); // yes, that's it
    }
  }
}
```

### Tables
The [`TablePattern`](http://msdn.microsoft.com/en-us/library/system.windows.automation.tablepattern.aspx) is one that is used by `ListView`, `ListBox` and other various controls. Sometimes, however, controls that visually appear to be tables to not behave like `TablePattern` controls to automation. The [`DataGridView`](http://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.aspx) class is one of those. Here is an example of how do expose the `DataGridView` control to automation:

```csharp
using UIA.Extensions;

namespace YourApp
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();

      dataGridView.AsTable(); // yes, that's it
    }
  }
}
```

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Make sure you have tests
5. Make sure your tests pass
6. Push to the branch (`git push origin my-new-feature`)
7. Create new Pull Request
