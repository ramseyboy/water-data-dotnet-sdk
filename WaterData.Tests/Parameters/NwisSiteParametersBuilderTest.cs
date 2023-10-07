using WaterData.Models.Codes;
using WaterData.Parameters.Site;

namespace WaterData.Tests.Parameters;

public class NwisSiteParametersBuilderTest
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
        var uri = new NwisSiteParametersBuilder()
            .StateCode(code)
            .BuildParameters();

        Assert.NotNull(uri);
        Assert.NotNull(uri.Uri);
        Assert.NotNull(uri.Uri.Query);
        Assert.Contains("TX", uri.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
    }

}
