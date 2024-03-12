using System;
using System.Windows.Input;
using Nasa.Models.Services;
using ReactiveUI;

namespace Nasa.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;
    private readonly PicturesDb Db = new();

    public MainWindowViewModel()
    {
        MainView = new MainViewModel(Db);
        _contentViewModel = MainView;
        CalendarViewCommand = ReactiveCommand.Create(CalendarViewSwitch);
        FavoritesViewCommand = ReactiveCommand.Create(FavoritesViewSwitch);
        MainViewCommand = ReactiveCommand.Create(MainViewSwitch);
    }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    private MainViewModel MainView { get; }

    public ICommand CalendarViewCommand { get; }

    public ICommand MainViewCommand { get; }
    public ICommand FavoritesViewCommand { get; }

    private void CalendarViewSwitch()
    {
        ContentViewModel = new CalendarViewModel(ContentViewModel, Db);
    }

    private void MainViewSwitch()
    {
        ContentViewModel = new MainViewModel(Db);
    }

    private void FavoritesViewSwitch()
    {
        ContentViewModel = new FavoritesViewModel();
    }

}