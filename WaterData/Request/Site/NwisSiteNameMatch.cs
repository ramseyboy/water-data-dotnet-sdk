using System.ComponentModel;

namespace WaterData.Request.Site;

public enum NwisSiteNameMatch
{
    [Description("any")]
    Any,
    [Description("start")]
    Start,
    [Description("exact")]
    Exact
}