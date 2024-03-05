using WaterData.Exceptions;
using WaterData.Extensions;
using WaterData.Models.Codes;

namespace WaterData.Request.Codes;

public class NwisParameterCodesRequestBuilder: NwisCodesRequestBuilder<NwisParameterCode>
{
    private NwisParameterCodeGrouping[]? _groupings;

    internal NwisParameterCodesRequestBuilder(string fileName) : base(fileName)
    {
    }
    
    public NwisParameterCodesRequestBuilder CodeGroupings(params NwisParameterCodeGrouping[] codeGroupings)
    {
        if (!codeGroupings.Any())
        {
            throw new RequestBuilderException("Parameter code grouping cannot be empty", nameof(codeGroupings));
        }
        _groupings = codeGroupings;
        return this;
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
}
