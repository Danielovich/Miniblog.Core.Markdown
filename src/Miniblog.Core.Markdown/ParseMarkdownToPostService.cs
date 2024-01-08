namespace Miniblog.Core.Markdown;

using Microsoft.VisualBasic;

public interface IParseMarkdownToPost
{
    Task<List<MarkdownPost>> ParseMarkdownToPost();
}

public class ParseMarkdownToPostService : IParseMarkdownToPost
{
    private readonly IGithubContentsService githubContentsService;
    private readonly IDownloadMarkdown markdownDownloadService;
    public ParseMarkdownToPostService(IGithubContentsService githubContentsService, IDownloadMarkdown markdownDownloadService)
    {
        this.markdownDownloadService = markdownDownloadService;
        this.githubContentsService = githubContentsService;
    }

    public async Task<List<MarkdownPost>> ParseMarkdownToPost()
    {
        await githubContentsService.LoadContents();

        var contents = await markdownDownloadService.DownloadMarkdownAsync(
            githubContentsService.GithubContents.Select(d => new Uri(d.DownloadUrl)));

        return await ParseMarkdownToPost(contents);
    }

    public async Task<List<MarkdownPost>> ParseMarkdownToPost(List<string> markdownFiles)
    {
        var intermediatePosts = new List<MarkdownPost>();

        foreach (var item in markdownFiles)
        {
            var parser = new MarkdownBlogpostParser(item);

            await parser.ParseCommentsAsPropertiesAsync();
            await parser.ParseContent();

            intermediatePosts.Add(parser.MarkdownPost);
        }

        return intermediatePosts;
    }
}
