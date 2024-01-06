namespace Miniblog.Core.Markdown;

public interface IGithubContentsApi
{
    IReadOnlyList<GitHubContentsApiResponse> GithubContents { get; }

    Task LoadContents();
}
