namespace Miniblog.Core.Markdown;

public interface IDownloadMarkdown
{
    Task<List<string>> DownloadMarkdownAsync(IEnumerable<Uri> uris);
}

public class DownloadMarkdownService : IDownloadMarkdown
{
    private readonly HttpClient httpClient;

    public DownloadMarkdownService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<string>> DownloadMarkdownAsync(IEnumerable<Uri> uris)
    {
        var internalList = new List<string>();

        foreach (var uri in uris)
        {
            try
            {
                internalList.Add(
                    await DownloadMarkdownAsync(uri)
                );
            }
            catch (HttpRequestException hre)
            {
                internalList.Add($"Error: {hre}");
            }
            catch(Exception e)
            {
                internalList.Add($"Error: {e}");
            }
        }

        return internalList;
    }

    private async Task<string> DownloadMarkdownAsync(Uri uri) {
        var result = await httpClient.GetAsync(uri);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Could not download file at {uri}", null, result.StatusCode);
        }

        return await result.Content.ReadAsStringAsync();
    }
}
