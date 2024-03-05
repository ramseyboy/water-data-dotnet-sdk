using WaterData.Exceptions;
using WaterData.Nwis.Models.Codes;
using WaterData.Request;

namespace WaterData.Nwis.Codes;

public class NwisCodesRequestBuilder<T> : WaterDataRequestBuilder where T : NwisCode
{
    private readonly string _fileName;

    internal NwisCodesRequestBuilder(string fileName)
    {
        _fileName = fileName;
    }

    protected virtual Func<T, bool>? WhereClauseDelegate => null;

    public override IWaterDataEnumerableRequest<T> BuildRequest()
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            throw new RequestBuilderException(
                "Must specify some type of code to retrieve in code request builder, found nothing.",
                nameof(_fileName));
        }

        return new NwisResourceFileRequest<T>(_fileName, WhereClauseDelegate);
    }
}
