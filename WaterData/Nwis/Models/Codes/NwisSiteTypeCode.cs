using CsvHelper.Configuration.Attributes;

namespace WaterData.Nwis.Models.Codes;

public struct NwisSiteTypeCode : NwisCode
{
    [Name("site_tp_cd")] public string Code { get; set; }
    [Name("site_tp_nm")] public string Name { get; set; }
    [Name("site_tp_srt_nu")] public string SortNu { get; set; }
    [Name("site_tp_vld_fg")] public string VldFg { get; set; }
    [Name("site_tp_prim_fg")] public string PrimFg { get; set; }
    [Name("site_tp_ln")] public string Label { get; set; }
    [Name("site_tp_ds")] public string Description { get; set; }
}
