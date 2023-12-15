namespace Miniblog.Core.Markdown.Services
{
    using Microsoft.Extensions.Hosting;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class PostCacheBackgroundService : BackgroundService
    {
        private readonly IMarkdownToBlogpostsService markdownBlogpostsService;
        private readonly IPostCache postCache;

        public PostCacheBackgroundService(IMarkdownToBlogpostsService markdownBlogpostsService, IPostCache postCache)
        {
            this.markdownBlogpostsService = markdownBlogpostsService;
            this.postCache = postCache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(30));
            while (!stoppingToken.IsCancellationRequested)
            {
                var posts = await markdownBlogpostsService.MarkdownToBlogpostAggregator();

                postCache.Posts.Clear();
                postCache.Posts.AddRange(posts);

                await timer.WaitForNextTickAsync(stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
    }
}
