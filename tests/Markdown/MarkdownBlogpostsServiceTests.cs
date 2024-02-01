namespace Miniblog.Core.Markdown.Tests;

using Miniblog.Core.Markdown;

public partial class MarkdownBlogpostsServiceTests
{
    [Theory, AutoMoqData]
    public async Task Parse_Markdown_To_Blogpost_ReturnsPosts(
        [Frozen] IGithubContentsService githubContentsService,
        [Frozen] IDownloadMarkdown onlineContentService)
    {
        // Arrange
        var sut = new ParseMarkdownToPostService(githubContentsService, onlineContentService);

        // Act
        var result = await sut.ParseMarkdownToPost(MarkdownStrings.MarkdownFileCollection());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MarkdownPost>>(result);
    }
}


