namespace Miniblog.Core.Services
{
    using Microsoft.Extensions.Configuration;

    using Miniblog.Core.Models;

    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class GitHubContentsService : IGithubContentsService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public IReadOnlyList<GitHubContentsApiResponse> GithubPosts { get; private set; } = default!;

        public GitHubContentsService(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            this.configuration = config;

            //if not configured, do nothing
            if (string.IsNullOrEmpty(this.configuration[Constants.Config.Blog.GitHubContentsUrl])) { return; }

            // retrieve references for github contents
            this.Initialize();
        }

        private void Initialize()
        {
            var loadPostReferences = Task.Run(async () => await this.LoadGithubPostsAsync());
            loadPostReferences.Wait();
        }

        // collect markdown blogposts from github.
        private async Task LoadGithubPostsAsync()
        {
            var response = await httpClient.GetAsync(Constants.Config.Blog.GitHubContentsUrl);
            var json = await response.Content.ReadAsStringAsync();

            var blogPostReferences = JsonSerializer.Deserialize<List<GitHubContentsApiResponse>>(json)
                ?? new List<GitHubContentsApiResponse>();

            // we are only interested in md files
            blogPostReferences.RemoveAll(p => !p.DownloadUrl.EndsWith(".md"));

            GithubPosts = blogPostReferences;
        }
    }
}
