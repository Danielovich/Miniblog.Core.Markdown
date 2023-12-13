namespace Miniblog.Core.Services
{
    using Miniblog.Core.Models;

    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class MarkdownBlogpostParser
    {
        private const string validMarkdownComment1 = "[//]: #";
        private const string validMarkdownComment2 = "[//]:#";

        public string Markdown { get; set; } = default!;
        public Post Post { get; private set; }

        public MarkdownBlogpostParser(string markdown)
        {
            ArgumentNullException.ThrowIfNull(markdown);

            this.Markdown = markdown;
            this.Post = new Post();
        }

        /// <summary>
        /// Parses comments as blog post properties
        /// If successful you can use Post from here on after.
        /// </summary>
        public async Task ParseCommentsAsPropertiesAsync()
        {
            using var reader = new StringReader(this.Markdown);

            // line of content from the Markdown
            string? line;

            // Convention is that all comments from top of the MarkDown is a future
            // post setting. As soon as the parser sees something else it breaks.
            while ((line = reader.ReadLine()) != null)
            {
                if (!(line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2)))
                {
                    break;
                }

                if (line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2))
                {
                    var markdownComment = new MarkdownComment(line);

                    // somewhat a tedious bit of code but it's easy to understand
                    // we start by looking at what type of comment we have on our hands
                    // from there we extract the comment value 
                    switch (markdownComment.GetValueBetweenFirstQuoteAndColon())
                    {
                        case "title":
                            Post.Title = markdownComment.AsPostProperty("title").Value;
                            break;
                        case "slug":
                            Post.Slug = markdownComment.AsPostProperty("slug").Value;
                            break;
                        case "pubDate":
                            Post.PubDate = markdownComment.AsPostProperty("pubDate").ParseToDate();
                            break;
                        case "lastModified":
                            Post.LastModified = markdownComment.AsPostProperty("lastModified").ParseToDate();
                            break;
                        case "excerpt":
                            Post.Excerpt = markdownComment.AsPostProperty("excerpt").Value;
                            break;
                        case "categories":
                            markdownComment.ToProperty("categories", ',').Select(n => n.Value).ToList().ForEach(Post.Categories.Add);
                            break;
                        case "isPublished":
                            Post.IsPublished = markdownComment.AsPostProperty("isPublished").ParseToBool();
                            break;
                        default:
                            break;
                    } 
                }
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Takes all content which is not markdown comments property
        /// </summary>
        public async Task ParseContent()
        {
            using var reader = new StringReader(this.Markdown);
            using var contenWriter = new StringWriter();

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!(line.StartsWith(validMarkdownComment1) || line.StartsWith(validMarkdownComment2)))
                {
                    break;
                }

                contenWriter.WriteLine(line);
            }

            Post.Content = contenWriter.ToString();

            await Task.CompletedTask;
        }
    }
}
