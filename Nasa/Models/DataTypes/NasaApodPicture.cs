using SQLite;

namespace Nasa.Models.DataTypes;

[Table("Pictures")]
public class NasaApodPicture
{
    [PrimaryKey] [Column("date")] public string date { get; set; }

    [Column("title")] public string title { get; set; }
    [Column("url")] public string url { get; set; }
    [Column("hdurl")] public string? hdurl { get; set; }
    [Column("media_type")] public string media_type { get; set; }
    [Column("explanation")] public string explanation { get; set; }
    [Column("thumbnail_url")] public string? thumbnail_url { get; set; }
    [Column("favorited")] public bool favorited { get; set; }
}