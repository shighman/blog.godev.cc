# Deploying blog.godev.cc to GitHub Pages

## Prerequisites

- [Git](https://git-scm.com/) installed
- A [GitHub](https://github.com/) account
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) installed (for local testing)

## 1. Initialise the Git repository

```bash
cd "C:\Users\shigh\Proton\My files\Code\Repos\blog.godev.cc"
git init
git add .
git commit -m "Initial commit: 90s Blazor WASM blog"
```

## 2. Create a GitHub repository

1. Go to https://github.com/new
2. Name the repository `blog.godev.cc`
3. Leave it **public** (required for GitHub Pages on free plans)
4. Do **not** initialise with a README, .gitignore, or licence (we already have files)
5. Click **Create repository**

Then push your local repo:

```bash
git remote add origin https://github.com/YOUR_USERNAME/blog.godev.cc.git
git branch -M main
git push -u origin main
```

## 3. Enable GitHub Pages

1. Go to your repository on GitHub
2. Navigate to **Settings** > **Pages**
3. Under **Build and deployment > Source**, select **GitHub Actions**
4. No further configuration needed — the workflow file at `.github/workflows/deploy.yml` handles everything

## 4. Set up the custom domain (blog.godev.cc)

### In your DNS provider

Add a **CNAME** record:

| Type  | Name   | Value                      | TTL  |
|-------|--------|----------------------------|------|
| CNAME | `blog` | `YOUR_USERNAME.github.io`  | 3600 |

> Replace `YOUR_USERNAME` with your actual GitHub username.

### In GitHub

1. Go to **Settings** > **Pages**
2. Under **Custom domain**, enter `blog.godev.cc` and click **Save**
3. Tick **Enforce HTTPS** (may take a few minutes to become available)
4. Wait for DNS propagation — usually a few minutes, can take up to 24 hours

### Verify DNS

You can check propagation with:

```bash
nslookup blog.godev.cc
```

It should resolve to GitHub's servers.

## 5. Trigger the first deployment

The GitHub Actions workflow runs automatically on every push to `main`. Your first push in step 2 will have already triggered it.

To check the status:

1. Go to your repository on GitHub
2. Click the **Actions** tab
3. You should see a **Deploy to GitHub Pages** workflow run
4. Once it shows a green tick, your site is live

## 6. Verify the deployment

Visit https://blog.godev.cc — you should see the 90s-styled blog.

Test these pages:

- https://blog.godev.cc/ (home)
- https://blog.godev.cc/blog (archive)
- https://blog.godev.cc/about (about page)
- https://blog.godev.cc/post/welcome-to-my-site (individual post)

## If NOT using a custom domain

If you want to host at `https://YOUR_USERNAME.github.io/blog.godev.cc/` instead:

1. Delete the file `blog.godev.cc/wwwroot/CNAME`
2. Edit `blog.godev.cc/wwwroot/index.html` and change:
   ```html
   <base href="/" />
   ```
   to:
   ```html
   <base href="/blog.godev.cc/" />
   ```
3. Skip step 4 entirely (no custom domain setup needed)

---

## Local development

To test locally before pushing:

```bash
cd "C:\Users\shigh\Proton\My files\Code\Repos\blog.godev.cc\blog.godev.cc"
dotnet run
```

Open the URL shown in the terminal (e.g. `https://localhost:5001`).

## Adding new blog posts

Each blog post is two files in `blog.godev.cc/wwwroot/posts/`. No C# changes needed.

### 1. Create the metadata file

Create `blog.godev.cc/wwwroot/posts/my-new-post.json`:

```json
{
  "slug": "my-new-post",
  "title": "My New Post Title!!!",
  "subtitle": "A subtitle for the post",
  "publishedDate": "1999-06-15T00:00:00",
  "author": "WebMaster9000",
  "tags": ["tag1", "tag2"]
}
```

### 2. Create the Markdown content file

Create `blog.godev.cc/wwwroot/posts/my-new-post.md`:

```markdown
This is my first **REAL** blog post!!!

### Why I Started Blogging

Because the internet is *AWESOME* and I have things to say!!!

- Reason one
- Reason two
- Reason three

---

Thanks for reading!!!
```

**Important:** Both filenames must match the `slug` value (e.g. `my-new-post.json` and `my-new-post.md`).

### 3. Push to main

```bash
git add blog.godev.cc/wwwroot/posts/my-new-post.json blog.godev.cc/wwwroot/posts/my-new-post.md
git commit -m "Add new blog post: my-new-post"
git push
```

### 4. Done

The GitHub Actions workflow automatically:
1. Scans all `.json` metadata files in the `posts/` folder
2. Regenerates `posts.json` (the manifest) from those files
3. Builds and deploys the site

The new post appears on the home page and blog archive without any code changes.

### Metadata JSON format reference

| Field           | Type     | Description                                      |
|-----------------|----------|--------------------------------------------------|
| `slug`          | string   | URL-safe identifier (must match filenames)        |
| `title`         | string   | Post title displayed on the site                  |
| `subtitle`      | string   | Secondary line shown under the title              |
| `publishedDate` | string   | ISO 8601 date (e.g. `"1999-06-15T00:00:00"`)     |
| `author`        | string   | Author name                                       |
| `tags`          | string[] | Array of tag strings                              |

### Markdown quick reference

| Syntax              | Result              |
|---------------------|---------------------|
| `**bold**`          | **bold**            |
| `*italic*`          | *italic*            |
| `### Heading`       | Heading (h3)        |
| `- item`            | Bullet list         |
| `1. item`           | Numbered list       |
| `---`               | Horizontal rule     |
| `` `code` ``        | Inline code         |
| `[text](url)`       | Link                |

## Troubleshooting

### Build fails in GitHub Actions

- Check the **Actions** tab for error logs
- Ensure the .NET SDK version in `deploy.yml` matches what the project targets (`10.0.x`)
- The `_framework` folder requires `.nojekyll` to exist — the workflow creates this automatically

### Site loads but shows a blank page

- Open browser dev tools (F12) > Console tab for errors
- Ensure the `<base href>` matches your hosting path (`/` for custom domain, `/repo-name/` otherwise)

### Custom domain not working

- Verify DNS CNAME record points to `YOUR_USERNAME.github.io`
- Check **Settings > Pages** shows the custom domain with a green tick
- Ensure `wwwroot/CNAME` contains exactly `blog.godev.cc` with no trailing newline or spaces

### Direct URL navigation shows 404

The workflow copies `index.html` to `404.html` so that GitHub Pages serves the Blazor app for any URL. If this isn't working, check that the **Copy index.html to 404.html** step succeeded in the Actions log.

### New post not appearing

- Ensure the JSON file is valid (check with a JSON validator)
- Ensure the filename matches the `slug` field
- Check the **Generate posts.json manifest** step in the Actions log — it prints the generated manifest
