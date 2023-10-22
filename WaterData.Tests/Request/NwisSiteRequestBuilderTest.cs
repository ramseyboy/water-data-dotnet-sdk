using WaterData.Models.Codes;
using WaterData.Request;
using WaterData.Request.Site;

namespace WaterData.Tests.Parameters;

public class NwisSiteRequestBuilderTest
{
    [Fact(DisplayName =
        "Given a state code and no other parameters, When a parameters are built, Then the resulting Uri should have the given state code")]
    public void TestStateCode()
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
        Assert.NotNull(request);
        Assert.Contains("TX", request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
    }

}
