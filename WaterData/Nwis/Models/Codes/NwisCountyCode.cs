using CsvHelper.Configuration.Attributes;

namespace WaterData.Nwis.Models.Codes;

public struct NwisCountyCode : NwisCode
{
    [Name("county_cd")] public string Code { get; set; }
    [Name("county_nm")] public string Label { get; set; }
}
