﻿using System.ComponentModel;

namespace WaterData.Parameters.Site;

public enum NwisSiteNameMatch
{
    [Description("any")]
    Any,
    [Description("start")]
    Start,
    [Description("exact")]
    Exact
}