namespace WaterData.Request;

public interface IWaterDataHttpRequest<T>: IWaterDataRequest, IWaterDataEnumerableRequest<T>
{
    public Task<HttpResponseMessage> GetHttpResponseAsync(CancellationToken cancellationToken = new());
}
