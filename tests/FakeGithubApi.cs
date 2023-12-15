namespace Miniblog.Core.Markdown.Tests;

internal class FakeGithubApi
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
