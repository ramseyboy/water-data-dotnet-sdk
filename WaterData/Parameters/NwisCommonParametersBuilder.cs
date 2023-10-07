using System.Text;
using WaterData.Models.Codes;

namespace WaterData.Parameters;

public abstract class NwisCommonParametersBuilder<T>: IParametersBuilder where T: IParametersBuilder
{
    public string ApiUrl => "https://waterservices.usgs.gov/nwis";

    [NwisQueryParameter("outputDataTypeCd", NwisParameterType.Output)]
    protected NwisDataCollectionTypeCode? _dataCollectionTypeCode;

    [NwisQueryParameter("format", NwisParameterType.Output)]
    protected string? _format = "rdb";

    [NwisQueryParameter("seriesCatalogOutput", NwisParameterType.Output)]
    protected bool _seriesCatalogOutput = false;

    public abstract T DataCollectionTypeCode(NwisDataCollectionTypeCode dataCollectionTypeCode);

    public abstract T SeriesCatalogOutput(bool seriesCatalogOutput);

    public abstract NwisParameters BuildParameters();

    protected string BuildCommonParameters()
    {
        var sb = new StringBuilder();
        return sb.ToString();
    }
}
