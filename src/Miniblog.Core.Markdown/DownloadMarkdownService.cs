namespace Miniblog.Core.Markdown;
public interface IDownloadMarkdown
{
    Task<List<MardownFile>> DownloadMarkdownAsync(IEnumerable<Uri> uris);
}

public class DownloadMarkdownService : IDownloadMarkdown
{
    private readonly HttpClient httpClient;

    public List<DownloadMarkdownExecption> DownloadExceptions { get; set; } = new();

    public DownloadMarkdownService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<MardownFile>> DownloadMarkdownAsync(IEnumerable<Uri> uris)
    {
        var markdownDowloads = new List<MardownFile>();

        foreach (var uri in uris.ByValidFileExtensions(MarkdownFileProperties.ValidFileExtensions))
        {
            try
            {
                markdownDowloads.Add(
                    await DownloadMarkdownAsync(uri)
                );
            }
            catch (HttpRequestException hre)
            {
                DownloadExceptions.Add(new DownloadMarkdownExecption($"{hre.Message}", hre));
            }
            catch (Exception e)
            {
                DownloadExceptions.Add(new DownloadMarkdownExecption($"{e.Message}", e));
            }
        }

        return markdownDowloads;
    }

    private async Task<MardownFile> DownloadMarkdownAsync(Uri uri)
    {
        var result = await httpClient.GetAsync(uri);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Could not download file at {uri}", null, result.StatusCode);
        }

        MardownFile markdownFile = new();
        markdownFile.Contents = await result.Content.ReadAsStringAsync();

        return markdownFile;
    }
}
