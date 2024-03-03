namespace WaterData.Request;

public interface IWaterDataHttpRequest<T>: IWaterDataRequest<T>
{
    public Task<HttpResponseMessage> GetHttpResponseAsync(CancellationToken cancellationToken = new());
}
