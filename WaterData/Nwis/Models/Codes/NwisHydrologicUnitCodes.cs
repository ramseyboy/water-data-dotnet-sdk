using CsvHelper.Configuration.Attributes;

namespace WaterData.Nwis.Models.Codes;

public struct NwisHydrologicUnitCodes : NwisCode
{
    [Name("huc")] public string Code { get; set; }
    [Name("basin")] public string Label { get; set; }
}
