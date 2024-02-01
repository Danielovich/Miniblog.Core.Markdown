namespace Miniblog.Core.Tests.Markdown;

using Miniblog.Core.Markdown.Markdown;

public class OnlineContentServiceTests : IDisposable
{
    public void Dispose()
    {
        HttpClient.Dispose();
    }
    private Mock<HttpMessageHandler> HttpMessageHandlerMock { get; set; } = default!;
    private HttpClient HttpClient { get; set; } = default!;

    private Fixture fixture = new Fixture();

    public OnlineContentServiceTests()
    {
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        HttpClient = new HttpClient(HttpMessageHandlerMock.Object);
    }

    private async Task SetupResponse(Func<HttpResponseMessage> response)
    {
        HttpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        await Task.CompletedTask;
    }

    private HttpResponseMessage HttpResponseMessageWithStringContent(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        return new HttpResponseMessage()
        {
            StatusCode = httpStatusCode,
            Content = new StringContent("string content")
        };
    }

    [Theory]
    [AutoData]
    public async Task On_UnSuccessful_Status_Code_Errors_Are_Present(List<Uri> listOfUris)
    {
        // Arrange

        //otherwise we do not enter actual method we want to test because it filters out non markdown files
        listOfUris.Add(new Uri("https://some.dk/domain/file.md")); 

        await SetupResponse(() => HttpResponseMessageWithStringContent(HttpStatusCode.Gone));

        var sut = new DownloadMarkdownService(this.HttpClient);

        // Act
        var result = await sut.DownloadMarkdownAsync(listOfUris);

        // Assert
        Assert.True(sut.DownloadExceptions.Any());
    }

    [Theory]
    [MarkdownUriAutoData]
    public async Task Posts_Are_Downloaded(List<Uri> listOfUris)
    {
        // Arrange
        await SetupResponse(() => HttpResponseMessageWithStringContent());

        var sut = new DownloadMarkdownService(this.HttpClient);

        //Act
        var contents = await sut.DownloadMarkdownAsync(listOfUris);

        // Assert
        this.HttpMessageHandlerMock
            .Protected()
            .Verify("SendAsync",
                Times.Exactly(3),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());

        Assert.True(contents.Count == 3);
        Assert.True(contents.All(a => a.Contents.Equals("string content")));
    }
}
