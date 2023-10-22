using WaterData.Exceptions;
using WaterData.Extensions;
using WaterData.Models.Codes;

namespace WaterData.Request.Codes;

public class NwisParameterCodesRequestBuilder: IWaterDataRequestBuilder<NwisParameterCode>
{
    private NwisParameterCodeGrouping[]? _groupings;

    public NwisParameterCodesRequestBuilder CodeGroupings(params NwisParameterCodeGrouping[] codeGroupings)
    {
        if (!codeGroupings.Any())
        {
            throw new RequestBuilderException("Parameter code grouping cannot be empty");
        }
        _groupings = codeGroupings;
        return this;
    }

    public IWaterDataRequest<NwisParameterCode> BuildRequest()
    {
        const string fileName = "parameter_cd_query.tsv";
        Func<NwisParameterCode, bool>? filter = null;
        if (_groupings is not null)
        {
            filter = code =>
            {
                var groupCodes = _groupings
                    .Select(g => g.GetDescription())
                    .ToList();
                return groupCodes.Exists(gc => code.Group.Contains(gc, StringComparison.InvariantCultureIgnoreCase));
            };
        }
        return new NwisResourceFileRequest<NwisParameterCode>(fileName, filter);
    }
}
