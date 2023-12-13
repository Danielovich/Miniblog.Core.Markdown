namespace Miniblog.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class OnlineContentService
    {
        private readonly HttpClient httpClient;

        public OnlineContentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<string>> DownloadContentAsync(IEnumerable<Uri> uris)
        {
            var internalList = new List<string>();

            foreach (var uri in uris)
            {
                try
                {
                    internalList.Add(
                        await DownloadContentAsync(uri)
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

        private async Task<string> DownloadContentAsync(Uri uri) {
            var getPost = await httpClient.GetAsync(uri);
            if (!getPost.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Could not download file at {uri}", null, getPost.StatusCode);
            }

            return await getPost.Content.ReadAsStringAsync();
        }
    }
}
