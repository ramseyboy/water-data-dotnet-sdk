using WaterData.Exceptions;
using WaterData.Nwis;
using WaterData.Nwis.Models.Codes;

namespace WaterData.Tests.Nwis.Site;

public class NwisSiteHydrologicUnitCodesRequestBuilderTest
{
    [InlineData(new[] {12})]
    [Theory(DisplayName =
        "Given 1 valid major hydrologic unit code, When a parameters are built, Then the resulting Uri should have the HUC")]
    public void TestHucMajorValid(int[] hucs)
    {
        var codes = hucs
            .Select(c => new NwisHydrologicUnitCodes
            {
                Code = c.ToString(),
                Label = "Test Code"
            })
            .Cast<NwisCode>()
            .ToArray();

        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(codes)
            .BuildRequest();

        Assert.NotNull(request);
        Assert.NotNull(request.Uri);
        Assert.Contains(codes[0].Code, request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
    }

    [InlineData(new[] {12010101})]
    [InlineData(new[] {12010101, 12010102})]
    [InlineData(new[] {12010101, 12010102, 12010103})]
    [Theory(DisplayName =
        "Given valid minor hydrologic unit codes, When a parameters are built, Then the resulting Uri should have the HUCs")]
    public void TestHucMinorValid(int[] hucs)
    {
        var codes = hucs
            .Select(c => new NwisHydrologicUnitCodes
            {
                Code = c.ToString(),
                Label = "Test Code"
            })
            .Cast<NwisCode>()
            .ToArray();

        var request = NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(codes)
            .BuildRequest();

        Assert.NotNull(request);
        Assert.NotNull(request.Uri);

        foreach (var code in codes)
        {
            Assert.Contains(code.Code, request.Uri.Query, StringComparison.InvariantCultureIgnoreCase);
        }
    }


    [InlineData(new[] {1})]
    [InlineData(new[] {123})]
    [InlineData(new[] {1234})]
    [InlineData(new[] {12345})]
    [InlineData(new[] {123456})]
    [InlineData(new[] {1234567})]
    [InlineData(new[] {123456789})]
    [InlineData(new[] {1234567891})]
    [InlineData(new[] {1, 12})]
    [InlineData(new[] {1, 12345678})]
    [Theory(DisplayName =
        "Given an invalid hydrologic unit code, When to short or long, the builder should not allow it")]
    public void TestHucLengthValidation(int[] hucs)
    {
        var codes = hucs
            .Select(c => new NwisHydrologicUnitCodes
            {
                Code = c.ToString(),
                Label = "Test Code"
            })
            .Cast<NwisCode>()
            .ToArray();

        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(codes));
    }

    [InlineData(new[] {12, 13})]
    [Theory(DisplayName =
        "Given more than 1 valid major hydrologic unit codes, When added, the builder should not allow it")]
    public void TestHucMajorCountValidation(int[] hucs)
    {
        var codes = hucs
            .Select(c => new NwisHydrologicUnitCodes
            {
                Code = c.ToString(),
                Label = "Test Code"
            })
            .Cast<NwisCode>()
            .ToArray();

        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(codes));
    }

    [InlineData(new[]
    {
        12010101, 12010102, 12010103, 12010104, 12010105, 12010106, 12010107, 12010108, 12010109, 12010110, 12010111
    })]
    [Theory(DisplayName =
        "Given more than 10 valid minor hydrologic unit codes, When added, the builder should not allow it")]
    public void TestHucMinorCountValidation(int[] hucs)
    {
        var codes = hucs
            .Select(c => new NwisHydrologicUnitCodes
            {
                Code = c.ToString(),
                Label = "Test Code"
            })
            .Cast<NwisCode>()
            .ToArray();

        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(codes));
    }

    [Fact(DisplayName =
        "Given no hydrologic unit codes, When added, the builder should not allow it")]
    public void TestHucNoneValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode());
    }

    [Fact(DisplayName =
        "Given null hydrologic unit codes, When added, the builder should not allow it")]
    public void TestHucNullValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(null!));
    }

    [Fact(DisplayName =
        "Given null containing array hydrologic unit codes, When added, the builder should not allow it")]
    public void TestHucNullArrayValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode([null]));
    }

    [Fact(DisplayName =
        "Given null code containing array hydrologic unit codes, When added, the builder should not allow it")]
    public void TestHucNullCodeArrayValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode([new NwisHydrologicUnitCodes()]));
    }

    [Fact(DisplayName =
        "Given empty array of hydrologic unit codes, When added, the builder should not allow it")]
    public void TestHucEmptyValidation()
    {
        Assert.Throws<RequestBuilderException>(() => NwisRequestBuilder
            .Builder()
            .Sites()
            .HydrologicUnitCode(Array.Empty<NwisCode>()));
    }
}
