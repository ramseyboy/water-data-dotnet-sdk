using System.ComponentModel;

namespace WaterData.Parameters.Site;

public enum NwisSiteStatus
{
    [Description("all")]
    All,
    [Description("active")]
    Active,
    [Description("inactive")]
    Inactive
}