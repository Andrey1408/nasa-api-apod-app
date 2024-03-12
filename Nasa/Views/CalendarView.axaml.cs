using System;
using Avalonia.Controls;

namespace Nasa.Views;

public partial class CalendarView : UserControl
{
    public CalendarView()
    {
        InitializeComponent();

        calendar.DisplayDateEnd = DateTime.Today;
    }
}