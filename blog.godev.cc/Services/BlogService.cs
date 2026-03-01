using System.Net.Http.Json;
using blog.godev.cc.Models;

namespace blog.godev.cc.Services;

public class BlogService
{
    private readonly HttpClient _http;
    private List<PostSummary>? _manifest;
    private readonly Dictionary<string, BlogPost> _postCache = new();

    public BlogService(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Loads the posts.json manifest. Cached after first call.
    /// </summary>
    public async Task<IReadOnlyList<PostSummary>> GetAllPostsAsync()
    {
        if (_manifest is null)
        {
            _manifest = await _http.GetFromJsonAsync<List<PostSummary>>("posts/posts.json") ?? [];
            _manifest = _manifest.OrderByDescending(p => p.PublishedDate).ToList();
        }
        return _manifest.AsReadOnly();
    }

    /// <summary>
    /// Returns the most recent N post summaries.
    /// </summary>
    public async Task<IReadOnlyList<PostSummary>> GetRecentPostsAsync(int count = 3)
    {
        var all = await GetAllPostsAsync();
        return all.Take(count).ToList().AsReadOnly();
    }

    /// <summary>
    /// Fetches and caches a full blog post by slug.
    /// </summary>
    public async Task<BlogPost?> GetPostBySlugAsync(string slug)
    {
        if (_postCache.TryGetValue(slug, out var cached))
            return cached;

        try
        {
            var json = await _http.GetFromJsonAsync<BlogPostJson>($"posts/{slug}.json");
            if (json is null)
                return null;

            var post = BlogPost.FromJson(json);
            _postCache[slug] = post;
            return post;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}
