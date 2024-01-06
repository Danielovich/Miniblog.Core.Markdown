namespace Miniblog.Core.Markdown.Tests;

public class MarkdownBlogpostsServiceTests
{
    [Theory, AutoMoqData]
    public async Task Parse_Markdown_To_Blogpost_ReturnsPosts(
        [Frozen] Mock<IGithubContentsApi> githubContentsService,
        [Frozen] Mock<IDownloadMarkdown> onlineContentService,
        IReadOnlyList<GitHubContentsApiResponse> githubResponse,
        MarkdownPostService service)
    {
        // Arrange
        githubContentsService.Setup(s => s.LoadContents()).Returns(Task.CompletedTask);
        githubContentsService.Setup(s => s.GithubContents).Returns(githubResponse);
        onlineContentService.Setup(s => s.DownloadMarkdownAsync(It.IsAny<IEnumerable<Uri>>()))
            .ReturnsAsync(MarkdownStrings.MarkdownStringCollection());

        // Act
        var result = await service.GetMarkdownPosts();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MarkdownPost>>(result);
        githubContentsService.Verify(s => s.LoadContents(), Times.Once);
        onlineContentService.Verify(s => s.DownloadMarkdownAsync(It.IsAny<IEnumerable<Uri>>()), Times.AtLeastOnce);
    }
}


