using WaterData.Nwis;
using WaterData.Nwis.Models.Codes;

namespace WaterData.Tests.Nwis;

[Collection("IntegrationTest")]
public class NwisRequestBuilderTest
{
    [Fact(DisplayName = "Given a valid request, When sent, Then a list of valid 'NwisSites' should be returned")]
    public async Task TestGetSites()
    {
        var countyRequest = NwisRequestBuilder
            .Builder()
            .CountyCodes()
            .BuildRequest();

        var countyCodes = await countyRequest.GetAsync();

        var travisCountyCode = countyCodes.ToList().Find(x => x.Code == "48453");

        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode(travisCountyCode)
            .BuildRequest();

        var sites = await request.GetAsync();
        Assert.NotNull(sites);
        Assert.NotEmpty(sites);
    }

    [Fact(DisplayName =
        "Given a valid request for county codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetCountyCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .CountyCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for state codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetStateCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .StateCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for hydrologic codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetHydrologicUnitCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .HydrologicUnitCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for Agency codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetAgencyCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .AgencyCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for Aquifer codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetAquiferCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .AquiferCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for Local Aquifer codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetLocalAquiferCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .LocalAquiferCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for SiteType codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetSiteTypeCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .SiteTypeCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for Parameter codes, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetParameterCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .ParameterCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for Parameter codes only for organics, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetParameterOrganicCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .ParameterCodes()
            .CodeGroupings(NwisParameterCodeGrouping.Organics)
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }

    [Fact(DisplayName =
        "Given a valid request for data collection codes only for organics, When sent, Then a list of valid 'NwisCode' should be returned")]
    public async Task TestGetDataCollectionCodes()
    {
        var request = NwisRequestBuilder
            .Builder()
            .DataCollectionTypeCodes()
            .BuildRequest();

        var codes = await request.GetAsync();
        Assert.NotNull(codes);
        Assert.NotEmpty(codes);
    }
}
