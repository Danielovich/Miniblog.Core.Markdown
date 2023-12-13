namespace tests;

public class OnlineContentServiceFixture
{
    public Mock<HttpMessageHandler> HttpMessageHandlerMock { get; private set; } = default!;
    public HttpClient HttpClient { get; private set; } = default!;

    public void SetupResponse(HttpResponseMessage response)
    {
        HttpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
    }

    public OnlineContentServiceFixture()
    {
        HttpMessageHandlerMock = new Mock<HttpMessageHandler>();
        HttpClient = new HttpClient(HttpMessageHandlerMock.Object);
    }
}
