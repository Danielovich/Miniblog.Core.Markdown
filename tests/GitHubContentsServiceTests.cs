namespace tests;

public class GitHubContentsServiceTests : GitHubContentsServiceFixture
{
    private readonly GitHubContentsServiceFixture _fixture;

    public GitHubContentsServiceTests(GitHubContentsServiceFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void GithubPosts_Are_Available()
    {
        // Act
        var sut = new GitHubContentsService(_fixture.HttpClient, _fixture.Configuration);

        // Assert
        Assert.True(sut.GithubPosts.Count > 0);
    }
}
