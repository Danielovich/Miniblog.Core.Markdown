namespace tests;

public class GitHubContentsServiceFixture
{
    public Mock<HttpMessageHandler> HttpMessageHandlerMock { get; private set; } = default!;
    public HttpClient HttpClient { get; private set; } = default!;
    public IConfiguration Configuration { get; set; } = default!;

    public GitHubContentsServiceFixture()
    {
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var gitHubResponse = new GitHubContentsApiResponse();

        HttpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(gitHubResponse.FakeGitHubApiContentsResponse);

        HttpClient = new HttpClient(HttpMessageHandlerMock.Object);

        var inMemorySettings = new Dictionary<string, string?> {
            {"blog:markdownUrl", "https://api.github.com/repos/Danielovich/danielovich.github.io/contents/_posts?ref=master"},
        };

        Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();       
    }
}
