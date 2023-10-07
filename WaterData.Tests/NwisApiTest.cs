using WaterData.Parameters.Site;

namespace WaterData.Tests;

public class NwisApiTest
{
    [Fact(DisplayName = "Given a valid request, When sent, Then a list of valid 'NwisSites' should be returned")]
    public async Task TestGetSites()
    {
        var nwisApi = NwisApi.Create();

        var parameters = new NwisSiteParametersBuilder()
            .CountyCode("48453")
            .BuildParameters();

        var sites = await nwisApi.GetSites(parameters);
        Assert.NotNull(sites);
    }

    [Fact(DisplayName = "Given a valid request for county codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetCountyCodes()
    {
        var nwisApi = NwisApi.Create();
        var codes = await nwisApi.GetCountyCodes();
        Assert.NotNull(codes);
    }

    [Fact(DisplayName = "Given a valid request for state codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetStateCodes()
    {
        var nwisApi = NwisApi.Create();
        var codes = await nwisApi.GetStateCodes();
        Assert.NotNull(codes);
    }

    [Fact(DisplayName = "Given a valid request for hydrologic codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetHydrologicUnitCodes()
    {
        var nwisApi = NwisApi.Create();
        var codes = await nwisApi.GetHydrologicUnitCodes();
        Assert.NotNull(codes);
    }
}
