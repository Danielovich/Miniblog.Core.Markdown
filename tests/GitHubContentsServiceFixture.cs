namespace tests
{
    using Microsoft.Extensions.Configuration;

    using Moq;
    using Moq.Protected;

    public class GitHubContentsServiceFixture
    {
        public Mock<HttpMessageHandler> HttpMessageHandlerMock { get; private set; }
        public HttpClient HttpClient { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public GitHubContentsServiceFixture()
        {
            HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var gitHubResponse = new GitHubPostsResponse();

            HttpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(gitHubResponse.FakeGitHubContentsResponse);

            HttpClient = new HttpClient(HttpMessageHandlerMock.Object);

            var inMemorySettings = new Dictionary<string, string?> {
                {"blog:markdownUrl", "https://api.github.com/repos/Danielovich/danielovich.github.io/contents/_posts?ref=master"},
            };

            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }
    }
}
