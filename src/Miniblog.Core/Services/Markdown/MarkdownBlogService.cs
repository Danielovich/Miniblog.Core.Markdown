namespace Miniblog.Core.Services.Markdown
{
    using Microsoft.AspNetCore.Http;

    using Miniblog.Core.Models;
    using Miniblog.Core.Services;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    public class MarkdownBlogService : IBlogService
    {
        private readonly IPostCache postCache;
        private readonly IHttpContextAccessor contextAccessor;


        public MarkdownBlogService(IPostCache postCache, IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.postCache = postCache;
        }

        public virtual Task<Post?> GetPostById(string id)
        {
            var isAdmin = this.IsAdmin();
            var post = this.postCache.Posts.FirstOrDefault(p => p.ID.Equals(id, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(
                post is null || post.PubDate > DateTime.UtcNow || !post.IsPublished && !isAdmin
                ? null
                : post);
        }

        public virtual Task<Post?> GetPostBySlug(string slug)
        {
            var isAdmin = this.IsAdmin();
            var post = this.postCache.Posts.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(
                post is null || post.PubDate > DateTime.UtcNow || !post.IsPublished && !isAdmin
                ? null
                : post);
        }

        /// <remarks>Overload for getPosts method to retrieve all posts.</remarks>
        public virtual IAsyncEnumerable<Post> GetPosts()
        {
            var isAdmin = this.IsAdmin();

            var posts = this.postCache.Posts
                .Where(p => p.PubDate <= DateTime.UtcNow && (p.IsPublished || isAdmin))
                .ToAsyncEnumerable();

            return posts;
        }

        public virtual IAsyncEnumerable<Post> GetPosts(int count, int skip = 0)
        {
            var isAdmin = this.IsAdmin();

            var posts = this.postCache.Posts
                .Where(p => p.PubDate <= DateTime.UtcNow && (p.IsPublished || isAdmin))
                .Skip(skip)
                .Take(count)
                .ToAsyncEnumerable();

            return posts;
        }

        public virtual IAsyncEnumerable<Post> GetPostsByCategory(string category)
        {
            var isAdmin = this.IsAdmin();

            var posts = from p in this.postCache.Posts
                        where p.PubDate <= DateTime.UtcNow && (p.IsPublished || isAdmin)
                        where p.Categories.Contains(category, StringComparer.OrdinalIgnoreCase)
                        select p;

            return posts.ToAsyncEnumerable();
        }

        public IAsyncEnumerable<Post> GetPostsByTag(string tag)
        {
            var isAdmin = this.IsAdmin();

            var posts = from p in this.postCache.Posts
                        where p.PubDate <= DateTime.UtcNow && (p.IsPublished || isAdmin)
                        where p.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)
                        select p;

            return posts.ToAsyncEnumerable();
        }
        [SuppressMessage(
            "Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "Consumer preference.")]
        public virtual IAsyncEnumerable<string> GetCategories()
        {
            var isAdmin = this.IsAdmin();

            return this.postCache.Posts
                .Where(p => p.IsPublished || isAdmin)
                .SelectMany(post => post.Categories)
                .Select(cat => cat.ToLowerInvariant())
                .Distinct()
                .ToAsyncEnumerable();
        }

        protected bool IsAdmin() => this.contextAccessor.HttpContext?.User?.Identity!.IsAuthenticated == true;

        public Task DeletePost(Post post) => throw new NotImplementedException();
        public IAsyncEnumerable<string> GetTags() => throw new NotImplementedException();
        public Task<string> SaveFile(byte[] bytes, string fileName, string? suffix = null) => throw new NotImplementedException();
        public Task SavePost(Post post) => throw new NotImplementedException();
    }
}
