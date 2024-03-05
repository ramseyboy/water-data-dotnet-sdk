using WaterData.Exceptions;
using WaterData.Nwis;
using WaterData.Nwis.Models.Codes;

namespace WaterData.Tests.Nwis.Site;

public class NwisSiteStateCodeRequestBuilderTest
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
        "Given empty state code, When added, the builder should not allow it")]
    public void TestStateEmptyValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .StateCode(new NwisStateCode()));
    }

    [Fact(DisplayName =
        "Given null state code, When added, the builder should not allow it")]
    public void TestStateNullValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .StateCode(null!));
    }
}
