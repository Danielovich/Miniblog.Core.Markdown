namespace Miniblog.Core.Markdown.Tests;

using Miniblog.Core.Markdown;

public partial class MarkdownBlogpostsServiceTests
{
    public static class MarkdownStrings
    {
        public static List<MardownFile> MarkdownFileCollection()
        {
            var markdownFile = Path.Combine(Environment.CurrentDirectory, "assets", "post.md");

            List<MardownFile> markdownFiles = new();
            markdownFiles.Add(new MardownFile() { Contents = File.ReadAllText(markdownFile) });

            return markdownFiles;
        }
    }
}


