﻿using CsvHelper.Configuration.Attributes;

namespace WaterData.Nwis.Models.Codes;

public struct NwisDataCollectionTypeCode : NwisCode
{
    [Name("code")] public string Code { get; set; }
    [Name("label")] public string Label { get; set; }
}
