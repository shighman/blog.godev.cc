using Microsoft.AspNetCore.Components;

namespace blog.godev.cc.Models;

public class BlogPost
{
    public required string Slug { get; init; }
    public required string Title { get; init; }
    public required string Subtitle { get; init; }
    public required DateTime PublishedDate { get; init; }
    public required string Author { get; init; }
    public required MarkupString ContentHtml { get; init; }
    public required string[] Tags { get; init; }
}
