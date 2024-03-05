using NetTopologySuite.Geometries;
using WaterData.Nwis;
using WaterData.Nwis.Models.Codes;

namespace WaterData.Tests.Nwis.Site;

public class NwisSiteRequestBuilderTest
{
    [Fact(DisplayName =
        "Given a state code, When a parameters are built, Then the resulting Uri should have the given state code")]
    public void TestStateCodeUri()
    {
        var code = new NwisStateCode
        {
            Code = "TX",
            Label = "Texas"
        };
        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .StateCode(code)
            .BuildRequest();

        Assert.NotNull(request);
        Assert.NotNull(request.Uri);
        Assert.Contains(code.Code, request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName =
        "Given a county code, When a parameters are built, Then the resulting Uri should have the county state code")]
    public void TestCountyCodeUri()
    {
        var code = new NwisCountyCode
        {
            Code = "48453",
            Label = "Travis County"
        };
        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode(code)
            .BuildRequest();

        Assert.NotNull(request);
        Assert.NotNull(request.Uri);
        Assert.Contains(code.Code, request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName =
        "Given a hydrologic unit code, When a parameters are built, Then the resulting Uri should have the HUC")]
    public void TestHucUri()
    {
        var code = new NwisHydrologicUnitCodes
        {
            Code = "12",
            Label = "Brazos headwaters"
        };
        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(code)
            .BuildRequest();

        Assert.NotNull(request);
        Assert.NotNull(request.Uri);
        Assert.Contains(code.Code, request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName =
        "Given a bounding box, When a parameters are built, Then the resulting Uri should have the bounding box xy values")]
    public void TestBoundingBoxRequestUri()
    {
        var envelope = new Envelope(
            new Coordinate(-97.960345, 30.207096),
            new Coordinate(-97.539809, 30.408796)
        );
        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .BoundingBox(envelope)
            .BuildRequest();

        Assert.NotNull(request);
        Assert.NotNull(request.Uri);
        Assert.Contains("-97.960345", request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("30.207096", request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("-97.539809", request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("30.408796", request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
    }
}
