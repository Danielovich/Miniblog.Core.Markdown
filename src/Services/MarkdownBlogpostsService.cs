namespace Miniblog.Core.Markdown.Services
{
    using Miniblog.Core.Models;
    using Miniblog.Core.Services;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMarkdownToBlogpostsService
    {
        Task<List<Post>> MarkdownToBlogpostAggregator();
    }

    public class MarkdownBlogpostsService : IMarkdownToBlogpostsService
    {
        private readonly IGithubContentsService githubContentsService;
        private readonly IOnlineContentService onlineContentService;
        public MarkdownBlogpostsService(IGithubContentsService githubContentsService, IOnlineContentService onlineContentService)
        {
            this.onlineContentService = onlineContentService;
            this.githubContentsService = githubContentsService;
        }

        public async Task<List<Post>> MarkdownToBlogpostAggregator()
        {
            await githubContentsService.LoadGithubPostsAsync();

            var contents = await onlineContentService.DownloadContentAsync(
                githubContentsService.GithubPosts.Select(d => new Uri(d.DownloadUrl)));

            var intermediatePosts = new List<Post>();

            foreach (var item in contents)
            {
                var parser = new MarkdownBlogpostParser(item);

                await parser.ParseCommentsAsPropertiesAsync();
                await parser.ParseContent();

                intermediatePosts.Add(parser.Post);
            }

            return intermediatePosts;
        }
    }
}
