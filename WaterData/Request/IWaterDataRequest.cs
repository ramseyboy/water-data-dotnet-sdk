namespace WaterData.Request;

public interface IWaterDataRequest<T>
{
    public Uri Uri { get; }

    public Task<List<T>> GetAsync(CancellationToken cancellationToken = new());
}
