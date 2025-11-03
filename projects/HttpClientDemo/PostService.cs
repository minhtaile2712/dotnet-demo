using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace HttpClientDemo;

public class PostService
{
    private readonly HttpClient _httpClient;
    private readonly PostOptions _options;

    public PostService(
        HttpClient httpClient,
        IOptions<PostOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
    }

    public async Task<List<Post>> GetPostsAsync(int userId = 1)
    {
        var query = new Dictionary<string, string?>
        {
            ["userId"] = userId.ToString(),
        };
        var url = QueryHelpers.AddQueryString("/posts", query);

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode) return new();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var document = await JsonDocument.ParseAsync(stream);
        var array = document.RootElement.EnumerateArray();

        var addresses = array.Select(e => new Post
        {
            UserId = e.GetProperty("userId").GetInt32(),
            Id = e.GetProperty("id").GetInt32(),
            Title = e.GetProperty("title").GetString(),
            Body = e.GetProperty("body").GetString(),
        }).ToList();

        return addresses;
    }
}
