using System.ComponentModel;

namespace WaterData.Nwis.Site;

public enum NwisSiteStatus
{
    [Description("all")] All,
    [Description("active")] Active,
    [Description("inactive")] Inactive
}
