namespace WaterData.Request;

public interface IWaterDataEnumerableRequest<T> : IWaterDataRequest
{
    public Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = new());
}
