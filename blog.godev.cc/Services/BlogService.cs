using blog.godev.cc.Models;
using Microsoft.AspNetCore.Components;

namespace blog.godev.cc.Services;

public class BlogService
{
    private readonly List<BlogPost> _posts =
    [
        new BlogPost
        {
            Slug = "welcome-to-my-site",
            Title = "Welcome to My AWESOME Site!!!",
            Subtitle = "You have entered the ZONE",
            PublishedDate = new DateTime(1999, 1, 15),
            Author = "WebMaster9000",
            Tags = ["intro", "welcome", "cool"],
            ContentHtml = new MarkupString("""
                <p>WELCOME 2 MY SITE!!!! This is the BEST site on the INTERNET!!!</p>
                <p>I worked SO HARD on this. Please sign my <b>GUESTBOOK</b>!!!</p>
                <hr>
                <p>This site is optimized for <b>800x600</b> resolution and Netscape Navigator 4.0.</p>
                <p>You are visitor number <span style="color:#ff0000;font-weight:bold;">000001337</span> !!! WOW!!!</p>
                <p>Here are some things I like:</p>
                <ul>
                    <li>The World Wide Web</li>
                    <li>Netscape Navigator</li>
                    <li>MIDI music (coming soon!)</li>
                    <li>Animated GIFs</li>
                    <li>Writing HTML by hand in Notepad</li>
                    <li>AOL chat rooms</li>
                    <li>The Information Superhighway</li>
                </ul>
                <p><b>PLEASE LINK TO MY SITE!!!</b> Copy this button to your page:</p>
                <p><i>[button goes here - under construction!!!]</i></p>
                """)
        },
        new BlogPost
        {
            Slug = "top-10-kewl-links",
            Title = "My Top 10 KEWL Links!!!",
            Subtitle = "Check out these RADICAL sites on the Web",
            PublishedDate = new DateTime(1999, 2, 3),
            Author = "WebMaster9000",
            Tags = ["links", "web", "cool", "surfing"],
            ContentHtml = new MarkupString("""
                <p>Here are the BEST websites on the internet right now (updated weekly!!):</p>
                <ol>
                    <li><b>Alta Vista</b> - THE search engine. Better than Yahoo!</li>
                    <li><b>Ask Jeeves</b> - For when you have QUESTIONS about stuff</li>
                    <li><b>GeoCities</b> - Where COOL people live on the web</li>
                    <li><b>Angelfire</b> - Almost as good as GeoCities tbh</li>
                    <li><b>Hampster Dance</b> - THE original viral site (so funny!!)</li>
                    <li><b>Space Jam</b> - STILL the greatest website ever made</li>
                    <li><b>Web Crawler</b> - Another search engine (backup to Alta Vista)</li>
                    <li><b>Yahoo!</b> - Very good web directory</li>
                    <li><b>Excite</b> - Has horoscopes and stuff!</li>
                    <li><b>My OTHER pages</b> - Under Construction!!! Check back l8r!!!</li>
                </ol>
                <hr>
                <p><i>Note: Best viewed with Netscape Navigator 4.0 or above at 800x600 resolution</i></p>
                <p><b>Do you have a cool site? Email me at webmaster9000@aol.com and I will add you to the list!!!</b></p>
                """)
        },
        new BlogPost
        {
            Slug = "dotnet-blazor-is-radical",
            Title = ".NET Blazor is TOTALLY RADICAL!!!",
            Subtitle = "Why Blazor is the future of the Information Superhighway",
            PublishedDate = new DateTime(1999, 3, 20),
            Author = "WebMaster9000",
            Tags = ["blazor", "dotnet", "programming", "radical", "tech"],
            ContentHtml = new MarkupString("""
                <p>OK so I just discovered this NEW technology called <b>Blazor</b> and it is
                TOTALLY AWESOME!!!</p>
                <p>You can make websites using C# instead of JavaScript!!! WOW!!! JavaScript
                is for NERDS. C# is the SUPERIOR language!!!</p>
                <h3>PROS of Blazor:</h3>
                <ul>
                    <li>No JavaScript (JavaScript is for NERDS)</li>
                    <li>C# is SUPERIOR in every way</li>
                    <li>Works in any browser (almost, you need WebAssembly)</li>
                    <li>Microsoft made it so it must be good</li>
                    <li>WebAssembly is THE FUTURE of the web</li>
                    <li>You can reuse your C# code from your desktop apps!!!</li>
                </ul>
                <h3>CONS of Blazor:</h3>
                <ul>
                    <li>Big download size (but my 56K modem handles it!! only takes 4 hours)</li>
                    <li>Not many tutorials yet (but I am writing some!! check back l8r)</li>
                    <li>Your boss probably doesn't know what WebAssembly is</li>
                </ul>
                <hr>
                <p>IN CONCLUSION: Blazor = RADICAL. That is all. Good day.</p>
                <p><b>******* PLEASE SIGN MY GUESTBOOK *******</b></p>
                """)
        },
        new BlogPost
        {
            Slug = "my-new-midi-collection",
            Title = "Check Out My NEW MIDI Collection!!!",
            Subtitle = "The best MIDI files on the entire internet",
            PublishedDate = new DateTime(1999, 4, 7),
            Author = "WebMaster9000",
            Tags = ["midi", "music", "cool", "downloads"],
            ContentHtml = new MarkupString("""
                <p>I have been collecting MIDI files for 3 years now and I have THE BEST
                collection on the entire internet!! Here are my top picks:</p>
                <ul>
                    <li>Aqua - Barbie Girl (my favorite!!)</li>
                    <li>Backstreet Boys - I Want It That Way</li>
                    <li>Celine Dion - My Heart Will Go On (from TITANIC!!!)</li>
                    <li>Spice Girls - Wannabe</li>
                    <li>Smashing Pumpkins - Bullet With Butterfly Wings</li>
                    <li>Star Wars Theme (epic!!)</li>
                    <li>The X-Files Theme (spooky!!)</li>
                    <li>Doom II - E1M1 (classic!!)</li>
                </ul>
                <p><b>NOTE:</b> Make sure your speakers are turned on when you visit my site!!!
                The MIDI plays automatically!!! (coming soon, still figuring out the HTML)</p>
                <hr>
                <p>If you have a cool MIDI I don't have, email me!!!</p>
                <p><i>webmaster9000@aol.com</i></p>
                """)
        },
        new BlogPost
        {
            Slug = "under-construction-update",
            Title = "Site Update - Still Under Construction!!!",
            Subtitle = "What I have been working on this month",
            PublishedDate = new DateTime(1999, 5, 1),
            Author = "WebMaster9000",
            Tags = ["update", "construction", "news"],
            ContentHtml = new MarkupString("""
                <p>HEY EVERYONE!!! I know I haven't updated in a while but I have been
                SUPER BUSY with school and stuff. Here is what I have been working on:</p>
                <ul>
                    <li><b>DONE:</b> Fixed the background color (was too dark before)</li>
                    <li><b>DONE:</b> Added the hit counter (we are at 1337 visitors!!!)</li>
                    <li><b>DONE:</b> Updated the links page</li>
                    <li><b>IN PROGRESS:</b> MIDI autoplay (almost got it working!!!)</li>
                    <li><b>IN PROGRESS:</b> Frames layout (gonna look SO COOL)</li>
                    <li><b>TODO:</b> Guestbook (need to find a free service)</li>
                    <li><b>TODO:</b> Animated GIF gallery</li>
                    <li><b>TODO:</b> Web ring - applying to the GeoCities Programmers ring</li>
                    <li><b>TODO:</b> More blog posts about cool tech stuff</li>
                </ul>
                <p>PLEASE come back and visit!!! Tell your friends about my site!!!
                Add me to your FAVORITES!!!</p>
                <hr>
                <p>P.S. If the page looks weird it is because I am still working on it.
                Try refreshing with CTRL+F5. If still broken, try Netscape Navigator.</p>
                """)
        }
    ];

    public IReadOnlyList<BlogPost> GetAllPosts() =>
        _posts.OrderByDescending(p => p.PublishedDate).ToList().AsReadOnly();

    public BlogPost? GetPostBySlug(string slug) =>
        _posts.FirstOrDefault(p => p.Slug == slug);

    public IReadOnlyList<BlogPost> GetRecentPosts(int count = 3) =>
        _posts.OrderByDescending(p => p.PublishedDate).Take(count).ToList().AsReadOnly();
}
