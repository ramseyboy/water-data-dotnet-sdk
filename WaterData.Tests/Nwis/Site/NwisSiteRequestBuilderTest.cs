using NetTopologySuite.Geometries;
using WaterData.Nwis;

namespace WaterData.Tests.Nwis.Site;

public class NwisSiteRequestBuilderTest
{
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
