using WaterData.Request;
using WaterData.Request.Codes;

namespace WaterData.Tests.Nwis.Codes;

public class HydrologicUnitCodesRequestTest
{
    [Fact(DisplayName =
        "Given a valid request for all hydrologic codes, When sent, Then a list of valid HUC should be returned with only major and minor codes")]
    public async Task TestGetHydrologicUnitCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .HydrologicUnitCodes()
            .CodeTypes(HydrologicUnitCodeTypes.All)
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
        Assert.All(codes, code => Assert.True(code.Code.Length is 2 or 8, "HUC is not 2 (major) or 8 (minor) chars"));
    }

    [Fact(DisplayName =
        "Given a valid request for hydrologic major codes, When sent, Then a list of valid HUC should be returned with only major codes")]
    public async Task TestGetMajorHydrologicUnitCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .HydrologicUnitCodes()
            .CodeTypes(HydrologicUnitCodeTypes.Major)
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
        Assert.All(codes, code => Assert.True(code.Code.Length is 2, "HUC is not 2 (major) chars"));
    }

    [Fact(DisplayName =
        "Given a valid request for minor hydrologic codes, When sent, Then a list of valid HUC should be returned with only minor codes")]
    public async Task TestGetMinorHydrologicUnitCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .HydrologicUnitCodes()
            .CodeTypes(HydrologicUnitCodeTypes.Minor)
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
        Assert.All(codes, code => Assert.True(code.Code.Length is 8, "HUC is not 8 (minor) chars"));
    }

    [Fact(DisplayName =
        "Given a valid request for basin name hydrologic codes, When sent, Then a list of valid HUC should be returned with only the named codes")]
    public async Task TestGetBasinNameHydrologicUnitCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .HydrologicUnitCodes()
            .BasinNames("Yellow House")
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
        Assert.All(codes,
            code => Assert.Contains("Yellow House", code.Label, StringComparison.InvariantCultureIgnoreCase));
    }

    [Fact(DisplayName =
        "Given a valid request for exact hydrologic codes, When sent, Then a list of valid HUC should be returned with only the exact codes")]
    public async Task TestGetExactHydrologicUnitCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .HydrologicUnitCodes()
            .ExactCodes("12")
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
        Assert.All(codes, code => Assert.Equal("12", code.Code));
    }
}
