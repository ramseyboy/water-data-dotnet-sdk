namespace WaterData.Request;

public interface IWaterDataRequest<T>
{
    public Uri Uri { get; }

    public Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = new());

    public Task<Stream> GetStreamAsync(CancellationToken cancellationToken = new());
}
