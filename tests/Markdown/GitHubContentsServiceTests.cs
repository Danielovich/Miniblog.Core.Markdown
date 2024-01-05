namespace Miniblog.Core.Tests.Markdown;
public class GitHubContentsServiceTests : IDisposable
{
    public void Dispose()
    {
        HttpClient.Dispose();
    }
    private Mock<HttpMessageHandler> HttpMessageHandlerMock { get; set; } = default!;
    private HttpClient HttpClient { get; set; } = default!;
    private IConfiguration Configuration { get; set; } = default!;

    public GitHubContentsServiceTests()
    {
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        HttpClient = new HttpClient(HttpMessageHandlerMock.Object);

        var inMemorySettings = new Dictionary<string, string?> {
            {"blog:markdownUrl", "https://api.github.com/repos/Danielovich/danielovich.github.io/contents/_posts?ref=master"},
        };

        Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    [Fact]
    public void GithubPosts_Are_Available()
    {
        // Arrange
        var gitHubResponse = new FakeGithubApi();

        HttpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(gitHubResponse.ContentsEndpoint);

        var sut = new GitHubApiService(this.HttpClient, this.Configuration);

        // Act
        var posts = sut.LoadContents();

        // Assert
        Assert.True(sut.GithubContents.Count > 0);
    }
}
