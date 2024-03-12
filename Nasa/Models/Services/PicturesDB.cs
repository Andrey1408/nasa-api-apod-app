using System;
using System.Collections.Generic;
using System.IO;
using Nasa.Models.DataTypes;
using SQLite;

namespace Nasa.Models.Services;

public class PicturesDb
{
    public const string DatabaseFilename = "MySQLite.db3";

    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    private readonly SQLiteConnection _connection;

    public PicturesDb()
    {
        //       string dbPath = Path.Combine (
        //           Environment.GetFolderPath (Environment.SpecialFolder.Personal),
        //           "Pictures.db");

        const string DatabaseFilename = "MySQLite.db3";
        const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;


        var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        System.IO.Directory.CreateDirectory(basePath);
        var DatabasePath = Path.Combine(basePath, DatabaseFilename);

        _connection = new SQLiteConnection(DatabasePath, Flags);
        _connection.CreateTable<NasaApodPicture>();
    }

    public static string DatabasePath
    {
        get
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, DatabaseFilename);
        }
    }

    public List<NasaApodPicture>? GetFavorites()
    {
        return _connection.Table<NasaApodPicture>().ToList();
    }

    public List<NasaApodPicture>? GetPicture(string date)
    {
        return _connection.Table<NasaApodPicture>().Where(t => t.date == date).ToList();
    }

    public void DeletePicture(string date)
    {
        _connection.Delete<NasaApodPicture>(date);
    }

    public void InsertPicture(NasaApodPicture picture)
    {
        _connection.Insert(picture);
    }
}