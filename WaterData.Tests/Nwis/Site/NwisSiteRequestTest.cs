using NetTopologySuite.Geometries;
using WaterData.Models.Codes;
using WaterData.Request;

namespace WaterData.Tests.Nwis.Site;

[Collection("IntegrationTest")]
public class NwisSiteRequestTest
{
    [Fact(DisplayName =
        "Given a bounding box and no other parameters, When a request is built, Then the resulting response should have only the sites within the bounding box")]
    public async Task TestBoundingBoxRequest()
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

        var response = await request.GetAsync();
        Assert.NotNull(response);
        Assert.NotEmpty(response);
    }

    [Fact(DisplayName =
        "Given a hydrologic unit code, When a request are built, Then the resulting response should have only the sites with the HUC")]
    public async Task TestHucRequest()
    {
        var code = new NwisHydrologicUnitCodes()
        {
            Code = "12",
            Label = "Brazos headwaters"
        };
        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(code)
            .BuildRequest();

        var response = await request.GetAsync();
        Assert.NotNull(response);
        Assert.NotEmpty(response);
    }
}
