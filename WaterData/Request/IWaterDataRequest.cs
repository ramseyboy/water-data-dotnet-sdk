namespace WaterData.Request;

public interface IWaterDataRequest
{
    public Uri Uri { get; }

    public Task<Stream> GetStreamAsync(CancellationToken cancellationToken = new());
}
