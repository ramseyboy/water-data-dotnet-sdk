using CsvHelper.Configuration.Attributes;

namespace WaterData.Models.Codes;

public struct NwisParameterCode: NwisCode
{
    [Name("parm_cd")]
    public string Code { get; set; }
    [Name("group")]
    public string Group { get; set; }
    [Name("parm_nm")]
    public string Label { get; set; }
    [Name("epa_equivalence")]
    public string EpaEquivalence { get; set; }
    [Name("result_statistical_basis")]
    public string StatisticalBasis { get; set; }
    [Name("result_time_basis")]
    public string TimeBasis { get; set; }
    [Name("result_weight_basis")]
    public string WeightBasis { get; set; }
    [Name("result_particle_size_basis")]
    public string ParticleSizeBasis { get; set; }
    [Name("result_sample_fraction")]
    public string SampleFraction { get; set; }
    [Name("result_temperature_basis")]
    public string TemperatureBasis { get; set; }
    [Name("CASRN")]
    public string Casrn { get; set; }
    [Name("SRSName")]
    public string SrsName { get; set; }
    [Name("parm_unit")]
    public string Unit { get; set; }
}
