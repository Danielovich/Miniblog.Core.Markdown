namespace Miniblog.Core.Markdown;

public interface IParseMarkdownToPost
{
    Task<List<MarkdownPost>> GetMarkdownPosts();
}

public class MarkdownPostService : IParseMarkdownToPost
{
    private readonly IGithubContentsApi githubContentsService;
    private readonly IDownloadMarkdown markdownDownloadService;
    public MarkdownPostService(IGithubContentsApi githubContentsService, IDownloadMarkdown markdownDownloadService)
    {
        this.markdownDownloadService = markdownDownloadService;
        this.githubContentsService = githubContentsService;
    }

    public async Task<List<MarkdownPost>> GetMarkdownPosts()
    {
        await githubContentsService.LoadContents();

        var contents = await markdownDownloadService.DownloadMarkdownAsync(
            githubContentsService.GithubContents.Select(d => new Uri(d.DownloadUrl)));

        var intermediatePosts = new List<MarkdownPost>();

        foreach (var item in contents)
        {
            var parser = new MarkdownBlogpostParser(item);

            await parser.ParseCommentsAsPropertiesAsync();
            await parser.ParseContent();

            intermediatePosts.Add(parser.MarkdownPost);
        }

        return intermediatePosts;
    }
}
