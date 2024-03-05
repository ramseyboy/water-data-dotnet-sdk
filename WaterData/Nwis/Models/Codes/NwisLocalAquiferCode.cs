using CsvHelper.Configuration.Attributes;

namespace WaterData.Nwis.Models.Codes;

public struct NwisLocalAquiferCode : NwisCode
{
    [Name("aqfr_cd")] public string Code { get; set; }
    [Name("aqfr_nm")] public string Label { get; set; }
    [Name("state_cd")] public string StateCode { get; set; }
}
