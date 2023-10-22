using System.ComponentModel;

namespace WaterData.Models.Codes;

public enum NwisParameterCodeGrouping
{
    [Description("Information")]
    Information,
    [Description("Physical")]
    Physical,
    [Description("Radiochemical")]
    Radiochemical,
    [Description("Inorganics")]
    Inorganics,
    [Description("Nutrient")]
    Nutrient,
    [Description("Biological")]
    Biological,
    [Description("Organics")]
    Organics,
    [Description("Stable Isotopes")]
    StableIsotopes,
    [Description("Population/Community")]
    PopulationCommunity,
    [Description("Toxicity")]
    Toxicity,
    [Description("Microbiological")]
    Microbiological
}
