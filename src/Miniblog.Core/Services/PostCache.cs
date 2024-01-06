namespace Miniblog.Core.Markdown.Services
{
    using Miniblog.Core.Models;

    using NUglify.Helpers;

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
            post.Slug = markdownPost.Slug;
            post.Excerpt = markdownPost.Excerpt;
            post.Title = markdownPost.Title;
            markdownPost.Tags.ForEach(post.Tags.Add);
            markdownPost.Categories.ForEach(post.Categories.Add);


            return post;
        }
    }
}
