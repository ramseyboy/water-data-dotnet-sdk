using CsvHelper.Configuration.Attributes;

namespace WaterData.Models.Codes;

public struct NwisStateCode: NwisCode
{
    [Name("code")]
    public string Code { get; set; }
    [Name("name")]
    public string Label { get; set; }
    [Name("id")]
    public string FipsCode { get; set; }
}
