using CsvHelper.Configuration.Attributes;

namespace WaterData.Models.Codes;

public struct NwisLocalAquiferCode: NwisCode
{
    [Name("")]
    public string Code { get; set; }
    [Name("")]
    public string Label { get; set; }
}