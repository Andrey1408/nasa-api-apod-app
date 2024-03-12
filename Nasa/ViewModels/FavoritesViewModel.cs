using System.Collections.ObjectModel;
using Nasa.Models.DataTypes;
using Nasa.Models.Services;

namespace Nasa.ViewModels;

public class FavoritesViewModel : ViewModelBase
{
    private readonly PicturesDb db = new();

    public FavoritesViewModel()
    {
        Pictures = new ObservableCollection<NasaApodPicture>(db.GetFavorites());
    }

    public ObservableCollection<NasaApodPicture>? Pictures { get; set; }
}