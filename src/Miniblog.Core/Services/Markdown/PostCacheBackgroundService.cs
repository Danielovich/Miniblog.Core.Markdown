using Miniblog.Core.Markdown;

namespace Miniblog.Core.Services.Markdown
{
    using Microsoft.Extensions.Hosting;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class PostCacheBackgroundService : BackgroundService
    {
        private readonly IParseMarkdownToPost markdownBlogpostsService;
        private readonly IPostCache postCache;

        public PostCacheBackgroundService(IParseMarkdownToPost markdownBlogpostsService, IPostCache postCache)
        {
            this.markdownBlogpostsService = markdownBlogpostsService;
            this.postCache = postCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(30));
            while (!stoppingToken.IsCancellationRequested)
            {
                var posts = await markdownBlogpostsService.ParseMarkdownToPost();

                postCache.Posts.Clear();
                posts.ForEach(markdownPost => postCache.Posts.Add(markdownPost.ConvertToPost()));

                await timer.WaitForNextTickAsync(stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
    }
}
