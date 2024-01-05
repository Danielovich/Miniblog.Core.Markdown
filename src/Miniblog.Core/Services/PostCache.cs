namespace Miniblog.Core.Markdown.Services
{
    using Miniblog.Core.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IPostCache
    {
        List<Post> Posts { get; set; }
    }

    public class PostCache : IPostCache
    {
        public List<Post> Posts { get; set; } = new List<Post>();
    }

    public static class PostConverter
    {
        public static Post ConvertToPost(this MarkdownPost markdownPost)
        {
            Post post = new();

            post.LastModified = markdownPost.LastModified;
            post.Content = markdownPost.Content;
            post.PubDate = markdownPost.PubDate;
            post.IsPublished = markdownPost.IsPublished;
            post.Categories.ToList().AddRange(markdownPost.Categories);
            post.Slug = markdownPost.Slug;
            post.Excerpt = markdownPost.Excerpt;
            post.Tags.ToList().AddRange(markdownPost.Tags);

            return post;
        }
    }
}
