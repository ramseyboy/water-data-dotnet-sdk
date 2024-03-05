using WaterData.Exceptions;
using WaterData.Nwis.Models.Codes;

namespace WaterData.Nwis.Codes;

public enum HydrologicUnitCodeTypes
{
    Major,
    Minor,
    All
}

public class NwisHydrologicUnitCodesRequestBuilder : NwisCodesRequestBuilder<NwisHydrologicUnitCodes>
{
    private string[] _basinNames = Array.Empty<string>();

    private string[] _exactCodes = Array.Empty<string>();
    private HydrologicUnitCodeTypes _types = HydrologicUnitCodeTypes.All;

    internal NwisHydrologicUnitCodesRequestBuilder(string fileName) : base(fileName)
    {
    }

    protected override Func<NwisHydrologicUnitCodes, bool> WhereClauseDelegate => code =>
    {
        var typesTest = _types switch
        {
            HydrologicUnitCodeTypes.Major => code.Code.Length is 2,
            HydrologicUnitCodeTypes.Minor => code.Code.Length is 8,
            HydrologicUnitCodeTypes.All => code.Code.Length is 2 or 8,
            _ => throw new ArgumentOutOfRangeException()
        };

        var namesTest = !_basinNames.Any() ||
                        _basinNames.Any(n => code.Label.Contains(n, StringComparison.OrdinalIgnoreCase));

        var codesTest = !_exactCodes.Any() || _exactCodes.Contains(code.Code, StringComparer.OrdinalIgnoreCase);

        return typesTest && namesTest && codesTest;
    };

    public NwisHydrologicUnitCodesRequestBuilder CodeTypes(HydrologicUnitCodeTypes types)
    {
        _types = types;
        return this;
    }

    public NwisHydrologicUnitCodesRequestBuilder BasinNames(params string[] basinNames)
    {
        if (basinNames is null || !basinNames.Any())
        {
            throw new RequestBuilderException("Basin names cannot be empty", nameof(basinNames));
        }

        _basinNames = basinNames;
        return this;
    }

    public NwisHydrologicUnitCodesRequestBuilder ExactCodes(params string[] codes)
    {
        if (codes is null || !codes.Any())
        {
            throw new RequestBuilderException("Exact codes cannot be empty", nameof(codes));
        }

        if (codes.Any(code => code.Length != 2 && code.Length != 8))
        {
            throw new RequestBuilderException("Exact codes must be either 2 (major) or 8 (minor) characters",
                nameof(codes));
        }

        _exactCodes = codes;
        return this;
    }
}
