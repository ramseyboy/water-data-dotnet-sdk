﻿using System.ComponentModel;

namespace WaterData.Nwis.Site;

public enum NwisSiteNameMatch
{
    [Description("any")] Any,
    [Description("start")] Start,
    [Description("exact")] Exact
}
