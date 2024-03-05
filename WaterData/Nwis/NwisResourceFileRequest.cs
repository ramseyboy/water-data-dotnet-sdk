using System.Reflection;
using WaterData.Extensions;
using WaterData.Request;
using WaterData.Serializers;

namespace WaterData.Nwis;

public class NwisResourceFileRequest<T> : IWaterDataEnumerableRequest<T>
{
    private readonly string _fileName;

    private readonly Func<T, bool>? _whereClauseDelegate;

    public NwisResourceFileRequest(string fileName, Func<T, bool>? whereClauseDelegate = null)
    {
        _fileName = fileName;
        _whereClauseDelegate = whereClauseDelegate;
    }

    public Uri Uri => new($"file://{_fileName}");

    public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = new())
    {
        var stream = await GetStreamAsync(cancellationToken);
        return await RdbReader.ReadAsync(stream, _whereClauseDelegate, cancellationToken);
    }

    public async Task<Stream> GetStreamAsync(CancellationToken cancellationToken = new())
    {
        return await Assembly.GetExecutingAssembly().GetResourceStream(_fileName);
    }
}
