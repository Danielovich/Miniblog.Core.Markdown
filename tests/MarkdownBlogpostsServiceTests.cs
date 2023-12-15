namespace Miniblog.Core.Markdown.Tests;

using Miniblog.Core.Services;

public class MarkdownBlogpostsServiceTests
{
    [Theory, AutoMoqData]
    public async Task MarkdownToBlogpostAggregator_ReturnsPosts(
        [Frozen] Mock<IGithubContentsService> mockGithubContentsService,
        [Frozen] Mock<IOnlineContentService> mockOnlineContentService,
        MarkdownStrings markdownStringCollection,
        MarkdownBlogpostsService service,
        List<GitHubContentsApiResponse> fakeGithubPosts)
    {
        // Arrange
        mockGithubContentsService.Setup(s => s.LoadGithubPostsAsync()).Returns(Task.CompletedTask);
        mockGithubContentsService.Setup(s => s.GithubPosts).Returns(fakeGithubPosts);
        mockOnlineContentService.Setup(s => s.DownloadContentAsync(It.IsAny<IEnumerable<Uri>>()))
            .ReturnsAsync(markdownStringCollection.MarkdownStringCollection());

        // Act
        var result = await service.MarkdownToBlogpostAggregator();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Post>>(result);
        mockGithubContentsService.Verify(s => s.LoadGithubPostsAsync(), Times.Once);
        mockOnlineContentService.Verify(s => s.DownloadContentAsync(It.IsAny<IEnumerable<Uri>>()), Times.AtLeastOnce);
    }

    public class MarkdownStrings
    {
        public List<string> MarkdownStringCollection()
        {
            var markdownFile = Path.Combine(Environment.CurrentDirectory, "assets", "post.md");

            List<string> markdownStrings = new List<string>();
            markdownStrings.Add(File.ReadAllText(markdownFile));

            return markdownStrings;
        }
    }
}


