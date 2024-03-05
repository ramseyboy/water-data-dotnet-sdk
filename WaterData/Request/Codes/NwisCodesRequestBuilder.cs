using WaterData.Exceptions;
using WaterData.Models.Codes;

namespace WaterData.Request.Codes;

public class NwisCodesRequestBuilder<T>: WaterDataRequestBuilder where T: NwisCode
{
    private readonly string _fileName;

    internal NwisCodesRequestBuilder(string fileName)
    {
        _fileName = fileName;
    }

    public override IWaterDataEnumerableRequest<T> BuildRequest()
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            throw new RequestBuilderException("Must specify some type of code to retrieve in code request builder, found nothing.", nameof(_fileName));
        }
        return new NwisResourceFileRequest<T>(_fileName, WhereClauseDelegate);
    }

    protected virtual Func<T, bool>? WhereClauseDelegate => null;
}
