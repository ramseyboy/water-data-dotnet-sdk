using CsvHelper.Configuration.Attributes;

namespace WaterData.Models.Codes;

public struct NwisLocalAquiferCode: NwisCode
{
    [Name("aqfr_cd")]
    public string Code { get; set; }
    [Name("aqfr_nm")]
    public string Label { get; set; }
    [Name("state_cd")]
    public string StateCode { get; set; }
}
