# UIA.Extensions

`UIA.Extensions` is a .NET library that assists in exposing window controls to [Automation](http://msdn.microsoft.com/en-us/library/ms747327.aspx).

## Supported Controls

`UIA.Extensions` support the following:

### Value Pattern
The [`ValuePattern`](http://msdn.microsoft.com/en-us/library/system.windows.automation.valuepattern.aspx) allows you to get an set the value of a control. Not all controls support this pattern out of the box. Here is an example of how you might use `UIA.Extensions` to expose a [`MonthCalendar`](http://msdn.microsoft.com/en-us/library/system.windows.forms.monthcalendar.aspx) control to automation:

```csharp
public MainForm
{
  InitializeComponent();
  
  monthCalendar.AsValueControl(
    () => monthCalendar.SelectionStart.ToString() /* getter */,
    x => monthCalendar.SetDate(DateTime.Parse(x)) /* setter */);
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
