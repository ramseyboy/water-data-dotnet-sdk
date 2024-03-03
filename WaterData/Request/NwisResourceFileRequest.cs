using System.Reflection;
using WaterData.Extensions;
using WaterData.Serializers;

namespace WaterData.Request;

public class NwisResourceFileRequest<T>: IWaterDataRequest<T>
{
    private readonly string _fileName;

    private readonly Func<T, bool>? _whereClauseDelegate;

    public NwisResourceFileRequest(string fileName, Func<T, bool>? whereClauseDelegate = null)
    {
        _fileName = fileName;
        _whereClauseDelegate = whereClauseDelegate;
    }

    public Uri Uri => new($"file://{_fileName}");

    public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = new ())
    {
        var stream = await GetStreamAsync(cancellationToken);
        var codes = await RdbReader.ReadAsync<T>(stream, cancellationToken);
        if (_whereClauseDelegate is not null)
        {
            codes = codes.Where(_whereClauseDelegate).ToList();
        }
        return codes.ToList();
    }

    public async Task<Stream> GetStreamAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await Assembly.GetExecutingAssembly().GetResourceStream(_fileName);
    }
}
