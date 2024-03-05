using System.ComponentModel;

namespace WaterData.Nwis.Site;

public enum NwisSiteOutput
{
    [Description("basic")] Basic,
    [Description("expanded")] Expanded
}
