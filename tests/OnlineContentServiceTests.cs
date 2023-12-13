namespace tests;

public class OnlineContentServiceTests : IClassFixture<OnlineContentServiceFixture>
{
    private readonly OnlineContentServiceFixture onlineContentServiceFixture;

    public OnlineContentServiceTests(OnlineContentServiceFixture onlineContentServiceFixture)
    {
        this.onlineContentServiceFixture = onlineContentServiceFixture;
    }

    [Fact]
    public async Task Posts_Are_Downloaded()
    {
        // Arrange
        var listOfUris = new List<Uri>()
            {
                new Uri("https://some.markdown"),
                new Uri("https://someother.markdown"),
                new Uri("https://somemore.markdown")
            };

        var sut = new OnlineContentService(onlineContentServiceFixture.HttpClient);

        //Act
        var contents = await sut.DownloadContentAsync(listOfUris);

        // Assert
        onlineContentServiceFixture.HttpMessageHandlerMock
            .Protected()
            .Verify("SendAsync",
                Times.Exactly(3),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());

        Assert.True(contents.Count == 3);
    }

    [Fact]
    public async Task Reads_Error_On_UnSuccessful_Status_Code()
    {
        // Arrange
        onlineContentServiceFixture.SetupResponse(
            new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Gone,
                Content = new StringContent("Redirected")
            }
        );

        var sut = new OnlineContentService(onlineContentServiceFixture.HttpClient);

        // Act
        var result = await sut.DownloadContentAsync(new List<Uri>() { new Uri("https://some.markdown") });

        // Assert
        Assert.StartsWith("Error: ", result[0], StringComparison.InvariantCultureIgnoreCase);
    }
}
