using WaterData.Exceptions;
using WaterData.Nwis;
using WaterData.Nwis.Models.Codes;

namespace WaterData.Tests.Nwis.Site;

public class NwisSiteCountyCodeRequestBuilderTest
{
        [InlineData(new[] {48453})]
    [InlineData(new[] {48453, 48455})]
    [InlineData(new[] {48453, 48455, 48457})]
    [Theory(DisplayName =
        "Given a county code, When a parameters are built, Then the resulting Uri should have the county state code")]
    public void TestCountyCodeUri(int[] counties)
    {
        var codes = counties
            .Select(c => new NwisCountyCode
            {
                Code = c.ToString(),
                Label = "Test Code"
            })
            .Cast<NwisCode>()
            .ToArray();

        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode(codes)
            .BuildRequest();

        Assert.NotNull(request);
        Assert.NotNull(request.Uri);
        foreach (var code in codes)
        {
            Assert.Contains(code.Code, request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    [InlineData(new[] {484531})]
    [InlineData(new[] {4845312, 4845})]
    [InlineData(new[] {484, 48, 4})]
    [Theory(DisplayName =
        "Given county codes with invalid length, When added, the builder should not allow it")]
    public void TestCountyCodeInvalidLength(int[] counties)
    {
        var codes = counties
            .Select(c => new NwisCountyCode()
            {
                Code = c.ToString(),
                Label = "Test Code"
            })
            .Cast<NwisCode>()
            .ToArray();

        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode(codes));
    }

    [Fact(DisplayName =
        "Given more than 20 county codes, When added, the builder should not allow it")]
    public void TestCountyCodeMaxValidation()
    {
        var codes = new List<NwisCode>();
        var rand = new Random();
        for (var i = 0; i < 21; i++)
        {
            codes.Add(new NwisCountyCode
            {
                Code = rand.Next(11111, 99999).ToString()
            });
        }

        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode(codes.ToArray()));
    }

    [Fact(DisplayName =
        "Given no county codes, When added, the builder should not allow it")]
    public void TestCountyCodeNoneValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode());
    }

    [Fact(DisplayName =
        "Given null county codes, When added, the builder should not allow it")]
    public void TestCountyCodeNullValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode(null!));
    }

    [Fact(DisplayName =
        "Given null containing array county codes, When added, the builder should not allow it")]
    public void TestCountyCodeNullArrayValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode([null]));
    }

    [Fact(DisplayName =
        "Given null code containing array county unit codes, When added, the builder should not allow it")]
    public void TestCountyCodeNullCodeArrayValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode([new NwisCountyCode()]));
    }

    [Fact(DisplayName =
        "Given empty array of county codes, When added, the builder should not allow it")]
    public void TestCountyCodeEmptyValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .CountyCode(Array.Empty<NwisCode>()));
    }
}
