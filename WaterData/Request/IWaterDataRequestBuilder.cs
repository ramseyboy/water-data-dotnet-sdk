namespace WaterData.Request;

public interface IWaterDataRequestBuilder<T>
{
    public new IWaterDataRequest<T> BuildRequest();
}
