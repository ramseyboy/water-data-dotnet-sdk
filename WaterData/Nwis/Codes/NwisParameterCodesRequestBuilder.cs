using WaterData.Exceptions;
using WaterData.Extensions;
using WaterData.Nwis.Models.Codes;

namespace WaterData.Nwis.Codes;

public class NwisParameterCodesRequestBuilder : NwisCodesRequestBuilder<NwisParameterCode>
{
    private NwisParameterCodeGrouping[]? _groupings;

    internal NwisParameterCodesRequestBuilder(string fileName) : base(fileName)
    {
    }

    protected override Func<NwisParameterCode, bool> WhereClauseDelegate => code =>
    {
        if (_groupings is null)
        {
            return true;
        }

        var groupCodes = _groupings
            .Select(g => g.GetDescription())
            .ToList();
        return groupCodes.Exists(gc => code.Group.Contains(gc, StringComparison.InvariantCultureIgnoreCase));
    };

    public NwisParameterCodesRequestBuilder CodeGroupings(params NwisParameterCodeGrouping[] codeGroupings)
    {
        if (!codeGroupings.Any())
        {
            throw new RequestBuilderException("Parameter code grouping cannot be empty", nameof(codeGroupings));
        }

        _groupings = codeGroupings;
        return this;
    }
}
