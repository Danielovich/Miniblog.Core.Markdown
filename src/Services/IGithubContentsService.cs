namespace Miniblog.Core.Services
{
    using Miniblog.Core.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGithubContentsService
    {
        IReadOnlyList<GitHubContentsApiResponse> GithubPosts { get; }

        Task LoadGithubPostsAsync();
    }
}
