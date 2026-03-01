using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;

namespace blog.godev.cc.Models;

/// <summary>
/// JSON-serializable post metadata used in the manifest (posts.json).
/// </summary>
public class PostSummary
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("subtitle")]
    public string Subtitle { get; set; } = string.Empty;

    [JsonPropertyName("publishedDate")]
    public DateTime PublishedDate { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = [];
}

/// <summary>
/// Full JSON-serializable post including HTML content.
/// Stored as individual JSON files in wwwroot/posts/{slug}.json.
/// </summary>
public class BlogPostJson : PostSummary
{
    [JsonPropertyName("contentHtml")]
    public string ContentHtml { get; set; } = string.Empty;
}

/// <summary>
/// Rendered blog post used by Razor pages. Created from BlogPostJson.
/// </summary>
public class BlogPost
{
    public string Slug { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
    public DateTime PublishedDate { get; init; }
    public string Author { get; init; } = string.Empty;
    public string[] Tags { get; init; } = [];
    public MarkupString ContentHtml { get; init; }

    public static BlogPost FromJson(BlogPostJson json) => new()
    {
        Slug = json.Slug,
        Title = json.Title,
        Subtitle = json.Subtitle,
        PublishedDate = json.PublishedDate,
        Author = json.Author,
        Tags = json.Tags,
        ContentHtml = new MarkupString(json.ContentHtml)
    };
}
