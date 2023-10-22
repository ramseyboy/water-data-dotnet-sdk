using System.Net;
using WaterData.Serializers;

namespace WaterData.Request;

public class NwisHttpRequest<T>: IWaterDataRequest<T>
{
    public Uri Uri { get; }

    private readonly HttpClient _httpClient;

    internal NwisHttpRequest(Uri uri)
    {
        Uri = uri;

        var handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        _httpClient = new HttpClient(handler)
        {
            DefaultRequestHeaders =
            {
                {"Accept-Encoding", "gzip, compress"}
            }
        };
    }

    public async Task<List<T>> GetAsync(CancellationToken cancellationToken = new())
    {
        var msg = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = Uri
        };

        var res = await _httpClient.SendAsync(msg, cancellationToken);
        return await RdbReader.ReadAsync<T>(await res.Content.ReadAsStreamAsync(cancellationToken), cancellationToken);
    }
}
