using System.ComponentModel;

namespace WaterData.Parameters.Site;

public enum NwisSiteOutput
{
    [Description("basic")]
    Basic,
    [Description("expanded")]
    Expanded
}