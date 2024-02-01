namespace Miniblog.Core.Markdown.Markdown;

using Miniblog.Core.Markdown;

public static class StartupExtensions
{
    public static IServiceCollection AddMarkdown(this IServiceCollection services)
    {
        services.AddHttpClient<GitHubApiService>();
        services.AddHttpClient<DownloadMarkdownService>();
        services.AddSingleton<IParseMarkdownToPost, ParseMarkdownToPostService>();
        services.AddSingleton<IDownloadMarkdown, DownloadMarkdownService>();
        services.AddSingleton<IGithubContentsService, GitHubApiService>();

        return services;
    }
}
