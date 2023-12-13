namespace Miniblog.Core.Services
{
    using Miniblog.Core.Models;

    using System.Collections.Generic;

    public interface IGithubContentsService
    {
        IReadOnlyList<GitHubContentsApiResponse> GithubPosts { get; }
    }
}
