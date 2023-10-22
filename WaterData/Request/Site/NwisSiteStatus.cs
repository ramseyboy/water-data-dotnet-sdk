using System.ComponentModel;

namespace WaterData.Request.Site;

public enum NwisSiteStatus
{
    [Description("all")]
    All,
    [Description("active")]
    Active,
    [Description("inactive")]
    Inactive
}
