using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Nasa.Models.DataTypes;

namespace Nasa.Models.Services;

public class HttpClientFactoryService
{
    private static readonly HttpClient _httpClient = new();
    private readonly JsonSerializerOptions _options = JsonSerializerOptions.Default;

    public async Task<NasaApodPicture> GetPictureByDate(string date)
    {
        var queryDict = new Dictionary<string, string?>
        {
            ["date"] = date
        };
        var query = QueryHelpers.AddQueryString(
            "https://api.nasa.gov/planetary/apod?api_key=wKDUapFXCWOKgQxp4Z5Ezl37Ex5Q2bnu8F9dzvA6", queryDict);
        using (var response = await _httpClient.GetAsync(query, HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var picture = await JsonSerializer.DeserializeAsync<NasaApodPicture>(stream, _options);
            return picture;
        }
    }
}