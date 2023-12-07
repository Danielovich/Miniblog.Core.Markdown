namespace tests
{
    using Miniblog.Core.Services;

    public class GitHubContentsTests : GitHubContentsServiceFixture
    {
        private readonly GitHubContentsServiceFixture _fixture;

        public GitHubContentsTests(GitHubContentsServiceFixture fixture)
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
}
