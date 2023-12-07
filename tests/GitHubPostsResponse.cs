namespace tests
{
    using System.Net;
    using System.Text;

    internal class GitHubPostsResponse
    {
        public HttpResponseMessage FakeGitHubContentsResponse()
        {
            // Return response from https://api.github.com/repos/Danielovich/danielovich.github.io/contents/_posts?ref=master
            var jsonResponseTestPath = Path.Combine(Environment.CurrentDirectory, "Assets", "contentsresponse.json");
            string jsonBlob = File.ReadAllText(jsonResponseTestPath);

            // Create the HttpResponseMessage
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonBlob, Encoding.UTF8, "application/json")
            };

            return response;
        }
    }
}
