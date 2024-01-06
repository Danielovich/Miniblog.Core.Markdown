namespace Miniblog.Core.Markdown.Markdown;

public static class StartupExtensions
{
    public static IServiceCollection AddMarkdown(this IServiceCollection services)
    {

        services.AddHttpClient<GitHubApiService>();
        services.AddHttpClient<DownloadMarkdownService>();
        services.AddSingleton<IParseMarkdownToPost, MarkdownPostService>();
        services.AddSingleton<IDownloadMarkdown, DownloadMarkdownService>();
        services.AddSingleton<IGithubContentsApi, GitHubApiService>();

        return services;
    }
}
