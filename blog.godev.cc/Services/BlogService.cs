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

    public async Task<IReadOnlyList<PostSummary>> GetAllPostsAsync()
    {
        if (_manifest is null)
        {
            _manifest = await _http.GetFromJsonAsync<List<PostSummary>>("posts/posts.json") ?? [];
            _manifest = _manifest.OrderByDescending(p => p.PublishedDate).ToList();
        }
        return _manifest.AsReadOnly();
    }

    public async Task<IReadOnlyList<PostSummary>> GetRecentPostsAsync(int count = 3)
    {
        var all = await GetAllPostsAsync();
        return all.Take(count).ToList().AsReadOnly();
    }

    public async Task<BlogPost?> GetPostBySlugAsync(string slug)
    {
        if (_postCache.TryGetValue(slug, out var cached))
            return cached;

        try
        {
            var summary = await _http.GetFromJsonAsync<PostSummary>($"posts/{slug}.json");
            if (summary is null)
                return null;

            var markdown = await _http.GetStringAsync($"posts/{slug}.md");

            var post = BlogPost.FromMarkdown(summary, markdown);
            _postCache[slug] = post;
            return post;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}
