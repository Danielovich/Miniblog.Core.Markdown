namespace Miniblog.Core.Markdown.Tests;
public partial class MarkdownBlogpostsServiceTests
{
    public static class MarkdownStrings
    {
        public static List<string> MarkdownStringCollection()
        {
            var markdownFile = Path.Combine(Environment.CurrentDirectory, "assets", "post.md");

            List<string> markdownStrings = new List<string>();
            markdownStrings.Add(File.ReadAllText(markdownFile));

            return markdownStrings;
        }
    }
}


