using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Nasa.Models.DataTypes;
using Nasa.Models.Services;
using ReactiveUI;

namespace Nasa.ViewModels;

public class MainViewModel : ViewModelBase
{
    private static DateTime _todayDate = DateTime.Today;
    private readonly PicturesDb _db;
    private readonly HttpClientFactoryService _httpClientFactory = new();
    private Task<Bitmap?> _imageFromWeb;
    private string _imageUrl;
    private NasaApodPicture _nasaApodPicture;


    public MainViewModel()
    {
        FavouriteCommand = ReactiveCommand.Create(Favourite);
        GetTodayImage();
        _db = new PicturesDb();
        
    }
    public MainViewModel(PicturesDb db)
    {
        FavouriteCommand = ReactiveCommand.Create(Favourite);
        GetTodayImage();
        _db = db;
    }

    public MainViewModel(PicturesDb db, DateTime date)
    {
        _todayDate = date;
        FavouriteCommand = ReactiveCommand.Create(Favourite);
        GetTodayImage();
        _db = db;
    }

    public string TodayDate => _todayDate.ToShortDateString();

    public NasaApodPicture NasaApodPicture
    {
        get => _nasaApodPicture;
        private set
        {
            if (value.hdurl != null)
                _imageUrl = value.hdurl;
            else
                _imageUrl = value.url;

            this.RaiseAndSetIfChanged(ref _nasaApodPicture, value);
        }
    }


    public Task<Bitmap?> ImageFromWeb
    {
        get => _imageFromWeb;
        private set => this.RaiseAndSetIfChanged(ref _imageFromWeb, value);
    }

    public ICommand FavouriteCommand { get; }

    private void Favourite()
    {
        _db.InsertPicture(NasaApodPicture);
    }

    private async Task GetTodayImage()
    {
        var picture = await _httpClientFactory.GetPictureByDate(_todayDate.ToString("yyyy-MM-dd"));
        NasaApodPicture = picture;
        ImageFromWeb = ImageHelper.LoadFromWeb(new Uri(_imageUrl));
    }
}