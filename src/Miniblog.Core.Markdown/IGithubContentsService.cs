namespace Miniblog.Core.Markdown;

public interface IGithubContentsService
{
    IReadOnlyList<GitHubContentsApiResponse> GithubContents { get; }

    Task LoadContents();
}
