namespace tests
{
    using System.Net;
    using System.Text;

    internal class GitHubContentsApiResponse
    {
        public HttpResponseMessage FakeGitHubApiContentsResponse()
        {
            // Return response from https://api.github.com/repos/Danielovich/danielovich.github.io/contents/_posts?ref=master
            var jsonResponseTestPath = Path.Combine(Environment.CurrentDirectory, "Assets", "contentsresponse.json");
            string jsonBlob = File.ReadAllText(jsonResponseTestPath);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonBlob, Encoding.UTF8, "application/json")
            };

            return response;
        }
    }

    internal class OnlineMarkdownContentResponse
    {
        public HttpResponseMessage FakeOnlineContentsResponse()
        {
            var markdownResponseTestPath = Path.Combine(Environment.CurrentDirectory, "Assets", "post.md");
            string markdownContent = File.ReadAllText(markdownResponseTestPath);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(markdownContent, Encoding.UTF8, "application/text")
            };

            return response;
        }
    }
}
