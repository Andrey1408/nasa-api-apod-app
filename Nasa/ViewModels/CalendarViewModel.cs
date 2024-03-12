using System;
using Nasa.Models.Services;
using ReactiveUI;

namespace Nasa.ViewModels;

public class CalendarViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;
    private readonly PicturesDb _db;
    private DateTime _selectedDate;

    public CalendarViewModel(ViewModelBase ContentViewModel, PicturesDb db)
    {
        _selectedDate = SelectedDate;
        _contentViewModel = ContentViewModel;
        _db = db;
        this.WhenAnyValue(vm => vm.SelectedDate).Subscribe(_ => ShowDateImage());
    }

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (value != default)
                this.RaiseAndSetIfChanged(ref _selectedDate, value);
            else
                _selectedDate = value;
        }
    }

    public void ShowDateImage()
    {
        _contentViewModel = new MainViewModel(_db, _selectedDate);
    }
}