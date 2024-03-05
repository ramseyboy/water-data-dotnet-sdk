using System.Net;
using WaterData.Request;
using WaterData.Serializers;

namespace WaterData.Nwis;

public class NwisHttpRequest<T> : IWaterDataHttpRequest<T>
{
    private readonly HttpClient _httpClient;

    public NwisHttpRequest(Uri uri)
    {
        Uri = uri;

        var handler = new HttpClientHandler
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

    public Uri Uri { get; }

    public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = new())
    {
        return await RdbReader.ReadAsync<T>(await GetStreamAsync(cancellationToken),
            cancellationToken: cancellationToken);
    }

    public async Task<HttpResponseMessage> GetHttpResponseAsync(CancellationToken cancellationToken = new())
    {
        var msg = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = Uri
        };
        return await _httpClient.SendAsync(msg, cancellationToken);
    }

    public async Task<Stream> GetStreamAsync(CancellationToken cancellationToken = new())
    {
        var res = await GetHttpResponseAsync(cancellationToken);
        return await res.Content.ReadAsStreamAsync(cancellationToken);
    }
}
