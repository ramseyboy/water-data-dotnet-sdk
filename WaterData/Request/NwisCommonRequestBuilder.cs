using System.Text;
using WaterData.Models.Codes;

namespace WaterData.Request;

public abstract class NwisCommonRequestBuilder<TResult, TBuilder>: IWaterDataRequestBuilder<TResult> where TBuilder: IWaterDataRequestBuilder<TResult>
{
    [NwisQueryParameter("outputDataTypeCd", NwisParameterType.Output)]
    protected NwisDataCollectionTypeCode? _dataCollectionTypeCode;

    [NwisQueryParameter("format", NwisParameterType.Output)]
    protected string? _format = "rdb";

    [NwisQueryParameter("seriesCatalogOutput", NwisParameterType.Output)]
    protected bool _seriesCatalogOutput = false;

    public abstract TBuilder DataCollectionTypeCode(NwisDataCollectionTypeCode dataCollectionTypeCode);

    public abstract TBuilder SeriesCatalogOutput(bool seriesCatalogOutput);

    public abstract IWaterDataRequest<TResult> BuildRequest();

    protected string BuildCommonParameters()
    {
        var sb = new StringBuilder();
        return sb.ToString();
    }
}
