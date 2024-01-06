namespace Miniblog.Core.Markdown;

public class GitHubApiService : IGithubContentsApi
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;

    public IReadOnlyList<GitHubContentsApiResponse> GithubContents { get; private set; } = default!;

    public GitHubApiService(HttpClient httpClient, IConfiguration config)
    {
        this.httpClient = httpClient;
        this.configuration = config;

        ArgumentException.ThrowIfNullOrWhiteSpace(this.configuration[Constants.Config.GitHubContentsUrl]);

        // github will not allow any requests without a user agent
        this.httpClient.DefaultRequestHeaders.Add("User-Agent", "Miniblog.Core.Markdown");
    }

    /// <summary>
    /// Calls 
    /// </summary>
    /// <returns></returns>
    public async Task LoadContents()
    {
        var response = await httpClient.GetAsync(this.configuration[Constants.Config.GitHubContentsUrl]);
        var json = await response.Content.ReadAsStringAsync();

        var apiResponse = JsonSerializer.Deserialize<List<GitHubContentsApiResponse>>(json)
            ?? new List<GitHubContentsApiResponse>();

        GithubContents = apiResponse;
    }
}
