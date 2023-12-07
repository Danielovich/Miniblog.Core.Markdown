namespace Miniblog.Core.Models
{
    //https://docs.github.com/en/rest/repos/contents?apiVersion=2022-11-28#about-repository-contents
    public class GitHubContentsApiResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; } = default!;
    }
}
