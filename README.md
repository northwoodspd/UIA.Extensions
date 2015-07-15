# UIA.Extensions

`UIA.Extensions` is a .NET library that assists in exposing window controls to [Automation](http://msdn.microsoft.com/en-us/library/ms747327.aspx).

## Supported Controls

* [Value Controls](#value-pattern)
* [RangeValue Controls](#spinners)
* [List Controls](#lists)
* [Table Controls](#tables)
  *  [DataGridView](#datagridview)
  *  [Custom Tables](#custom-tables)

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

### Lists
The [`SelectionPattern`](https://msdn.microsoft.com/en-us/library/system.windows.automation.selectionpattern(v=vs.110).aspx) / [`SelectionItemPattern`](https://msdn.microsoft.com/en-us/library/system.windows.automation.selectionitempattern(v=vs.110).aspx) are generally used by controls that have a list of available options as well as the ability to set / clear the currently selected items (think `ListBox`, `ComboBox`, etc.). While the `ComboBox` control exposes itself to automation slightly differently than a `ListBox` does, the underlying structure is the same.

The `AsList<T>` extension will allow you to expose a custom control that behaves similiarly to UI Automation as a `ListBox` or a `ComboBox` control. In order to fully expose your custom control as a list, you must implement the `ListInformation` / `ListItemInformation` classes to expose the behavior from your custom control. Here is a quick example of how one might tackle such an effort:

```csharp
using UIA.Extensions

namespace YourApp
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();

      customComboBox.AsList<CustomComboList>();
    }
  }
  
  class CustomComboList : ListInformation
  {
    private MyCustomCombo _comboBox;
    
    public CustomComboList(MyCustomCombo comboBox) : base(comboBox)
    {
      _comboBox = comboBox;
      IsRequired = false;
      CanSelectMultiple = true; // report that multiple selections are acceptable
    }
    
    public override List<ListItemInformation> ListItems
    {
      get { return _comboBox.Items.Select(CustomComboItem.Create).ToList(); }
    }
  }
  
  class CustomComboItem : ListItemInformation
  {
    private readonly MyCustomComboItem _item;
    private CustomComboItem(MyCustomComboItem item)
    {
      _item = item;
    }
    
    public ListItemInformation Create(MyCustomComboItem item)
    {
      return new CustomComboItem(item);
    }
    
    public override void Select()
    {
      _item.Select();
    }
    
    public override void AddToSelection()
    {
      Select();
    }
    
    public override void RemoveFromSelection()
    {
      _item.UnSelect();
    }
  }
}
```

### Tables

#### DataGridView
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

#### Custom Tables
Not everything is as easy as the `DataGridView` though. Suppose you have a custom grid control.
How would you expose it to automation? Simple. Just implement three interfaces:

```csharp
public interface TableInformation
{
  int RowCount { get; }
  int ColumnCount { get; }
  Control Control { get; }
  List<string> Headers { get; }
  List<RowInformation> Rows { get; }
}

public abstract class RowInformation
{
  public abstract string Value { get; }
  public abstract List<CellInformation> Cells { get; }
  public abstract void Select();
  public abstract bool IsSelected { get; }
}

public abstract class CellInformation
{
  public abstract string Value { get; }
  public abstract int Row { get;  }
  public abstract int Column { get;  }
  public abstract Rect Location { get; }
}

```

##### Example:
Suppose we have a custom class:

```csharp
public class CustomData
{
  public string Name { get; set; }
  public int Age { get; set; }
}
```

Here is how we could expose a `List<CustomData>` as a table on any control.

```csharp
public MainForm()
{
  ...
  anyControl.AsTable<CustomTableInformation>();
}

public class CustomTableInformation : TableInformation
{
  private readonly Control _control;
  private List<CustomData> _data;

  public CustomTableInformation(Control control)
  {
    _control = control;
    _data = new List<CustomData>
    {
      new CustomData { Name = "John Elway", Age = 53 },
      new CustomData { Name = "Levi Wilson", Age = 33 },
    };
  }

  public Control Control
  {
    get { return _control; }
  }

  public int ColumnCount
  {
    get { return 2; }
  }

  public List<string> Headers
  {
    get { return new List<string> { "Name", "Age" }; }
  }

  public int RowCount
  {
    get { return _data.Count; }
  }

  public List<RowInformation> Rows
  {
    get
    {
      var row = 0;
      return _data.Select(x => CustomRowInformation.FromData(x, row++)).ToList();
    }
  }
}

public class CustomRowInformation : RowInformation
{
  private readonly CustomData _data;
  private readonly int _row;

  private CustomRowInformation(CustomData data, int row)
  {
    _data = data;
    _row = row;
  }

  public static RowInformation FromData(CustomData data, int row)
  {
    return new CustomRowInformation(data, row);
  }

  public override string Value
  {
    get { return _data.Name; }
  }

  public override List<CellInformation> Cells
  {
    get
    {
      return new List<CellInformation>
      {
        CustomCellInformation.FromCustomData(_data.Name, 0, _row),
        CustomCellInformation.FromCustomData(_data.Age.ToString(), 1, _row),
      };
    }
  }

  public override void Select() { throw new NotImplementedException(); }
  public override bool IsSelected { get { throw new NotImplementedException(); } }
}

public class CustomCellInformation : CellInformation
{
  private readonly string _value;
  private readonly int _column;
  private readonly int _row;

  private CustomCellInformation(string value, int column, int row)
  {
    _value = value;
    _column = column;
    _row = row;
  }

  public static CellInformation FromCustomData(string value, int column, int row)
  {
    return new CustomCellInformation(value, column, row);
  }

  public override string Value { get { return _value; } }
  public override int Row { get { return _row; } }
  public override int Column { get { return _column; } }
  public override Rect Location { get { return Rect.Empty; } }
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
