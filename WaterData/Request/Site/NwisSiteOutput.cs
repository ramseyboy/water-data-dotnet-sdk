using System.ComponentModel;

namespace WaterData.Request.Site;

public enum NwisSiteOutput
{
    [Description("basic")]
    Basic,
    [Description("expanded")]
    Expanded
}