using System.Text.Json.Serialization;
using Markdig;
using Microsoft.AspNetCore.Components;

namespace blog.godev.cc.Models;

/// <summary>
/// JSON-serializable post metadata. Used both in the manifest (posts.json)
/// and in individual post metadata files (wwwroot/posts/{slug}.json).
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
/// Rendered blog post used by Razor pages. Built from PostSummary + raw Markdown.
/// </summary>
public class BlogPost
{
    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    public string Slug { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
    public DateTime PublishedDate { get; init; }
    public string Author { get; init; } = string.Empty;
    public string[] Tags { get; init; } = [];
    public MarkupString ContentHtml { get; init; }

    public static BlogPost FromMarkdown(PostSummary summary, string markdown) => new()
    {
        Slug = summary.Slug,
        Title = summary.Title,
        Subtitle = summary.Subtitle,
        PublishedDate = summary.PublishedDate,
        Author = summary.Author,
        Tags = summary.Tags,
        ContentHtml = new MarkupString(Markdown.ToHtml(markdown, Pipeline))
    };
}
