namespace Miniblog.Core.Markdown.Services
{
    using Miniblog.Core.Models;
    using System.Collections.Generic;

    public interface IPostCache
    {
        List<Post> Posts { get; set; }
    }

    public class PostCache : IPostCache
    {
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
