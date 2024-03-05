using CsvHelper.Configuration.Attributes;

namespace WaterData.Nwis.Models.Codes;

public struct NwisAgencyCode : NwisCode
{
    [Name("agency_cd")] public string Code { get; set; }
    [Name("party_nm")] public string Label { get; set; }
}
