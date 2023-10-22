using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;
using NetTopologySuite.Geometries;
using WaterData.Exceptions;
using WaterData.Extensions;
using WaterData.Models;
using WaterData.Models.Codes;

namespace WaterData.Request.Site;

public class NwisSiteRequestBuilder: IWaterDataRequestBuilder<NwisSite>
{
    private const string UrlPath = "/site";

    [NwisQueryParameter("stateCd", NwisParameterType.Major)]
    private NwisCode? _stateCode;

    [NwisQueryParameter("countyCd", NwisParameterType.Major)]
    private ICollection<NwisCode>? _countyCodes;

    [NwisQueryParameter("huc", NwisParameterType.Major)]
    private ICollection<NwisCode>? _hydrologicUnitCodes;

    [NwisQueryParameter("sites", NwisParameterType.Major)]
    private ICollection<string>? _siteNumbers;

    [NwisQueryParameter("bbox", NwisParameterType.Major)]
    private Envelope? _boundingBox;

    [NwisQueryParameter("siteOutput", NwisParameterType.Output)]
    private NwisSiteOutput _siteOutput = NwisSiteOutput.Basic;

    [NwisQueryParameter("startDt", NwisParameterType.Minor)]
    private DateTime? _startDate;

    [NwisQueryParameter("endDt", NwisParameterType.Minor)]
    private DateTime? _endDate;

    [NwisQueryParameter("period", NwisParameterType.Minor)]
    private TimeSpan? _period;

    [NwisQueryParameter("modifiedSince", NwisParameterType.Minor)]
    private TimeSpan? _modifiedSince;

    [NwisQueryParameter("siteName", NwisParameterType.Minor)]
    private string? _siteName;

    [NwisQueryParameter("siteNameMatchOperator", NwisParameterType.Minor)]
    private NwisSiteNameMatch? _siteNameMatchOperator = NwisSiteNameMatch.Start;

    [NwisQueryParameter("siteStatus", NwisParameterType.Minor)]
    private NwisSiteStatus _siteStatus = NwisSiteStatus.All;

    [NwisQueryParameter("siteType", NwisParameterType.Minor)]
    private ICollection<NwisCode>? _siteTypes;

    [NwisQueryParameter("hasDataTypeCd", NwisParameterType.Minor)]
    private NwisCode? _hasDataTypeCode;

    [NwisQueryParameter("parameterCd", NwisParameterType.Minor)]
    private NwisCode? _parameterCode;

    [NwisQueryParameter("agencyCd", NwisParameterType.Minor)]
    private NwisCode? _agencyCode;

    [NwisQueryParameter("altMin", NwisParameterType.Minor)]
    private double? _altMin;

    [NwisQueryParameter("altMax", NwisParameterType.Minor)]
    private double? _altMax;

    [NwisQueryParameter("drainAreaMin", NwisParameterType.Minor)]
    private double? _drainAreaMin;

    [NwisQueryParameter("drainAreaMax", NwisParameterType.Minor)]
    private double? _drainAreaMax;

    [NwisQueryParameter("aquiferCd", NwisParameterType.Minor)]
    private NwisCode? _aquiferCode;

    [NwisQueryParameter("localAquiferCd", NwisParameterType.Minor)]
    private NwisCode? _localAquiferCode;

    [NwisQueryParameter("wellDepthMin", NwisParameterType.Minor)]
    private double? _wellDepthMin;

    [NwisQueryParameter("wellDepthMax", NwisParameterType.Minor)]
    private double? _wellDepthMax;

    [NwisQueryParameter("holeDepthMin", NwisParameterType.Minor)]
    private double? _holeDepthMin;

    [NwisQueryParameter("holeDepthMax", NwisParameterType.Minor)]
    private double? _holeDepthMax;

    [NwisQueryParameter("outputDataTypeCd", NwisParameterType.Output)]
    private NwisCode? _dataCollectionTypeCode;

    [NwisQueryParameter("format", NwisParameterType.Output)]
    private string? _format = "rdb";

    [NwisQueryParameter("seriesCatalogOutput", NwisParameterType.Output)]
    private bool _seriesCatalogOutput = false;

    internal NwisSiteRequestBuilder()
    {
    }

    public NwisSiteRequestBuilder StateCode(NwisStateCode stateCode)
    {
        _stateCode = stateCode;
        return this;
    }

    public NwisSiteRequestBuilder CountyCode(params NwisCode[] countyCodes)
    {
        if (countyCodes is null || countyCodes.Length == 0 || countyCodes.Any(c => string.IsNullOrEmpty(c.Code)))
        {
            throw new RequestBuilderException("countyCodes parameter cannot be empty", nameof(countyCodes));
        }

        if (countyCodes.Length > 20)
        {
            throw new RequestBuilderException($"Cannot pass more than 20 countyCodes, found {countyCodes.Length}", nameof(countyCodes));
        }

        _countyCodes = countyCodes;
        return this;
    }

    public NwisSiteRequestBuilder HydrologicUnitCode(params NwisCode[] hydrologicUnitCodes)
    {
        if (hydrologicUnitCodes is null || hydrologicUnitCodes.Length == 0 || hydrologicUnitCodes.Any(c => string.IsNullOrEmpty(c.Code)))
        {
            throw new RequestBuilderException("hydrologicUnitCodes parameter cannot be empty or contain empty strings", nameof(hydrologicUnitCodes));
        }

        var majorCodeCount = hydrologicUnitCodes.Count(c => c.Code.Length == 2);
        var minorCodeCount = hydrologicUnitCodes.Count(c => c.Code.Length == 8);

        if (majorCodeCount == 0 && minorCodeCount == 0)
        {
            throw new RequestBuilderException(
                $@"hydrologicUnitCodes must be 2 chars for major code
                        and 8 chars for minor code, found no valid codes
                        in {string.Join(',', hydrologicUnitCodes.Select(c => c.Code))}", nameof(hydrologicUnitCodes));
        }

        if (majorCodeCount > 1)
        {
            throw new RequestBuilderException(
                $@"Only allowed 1 major 2 digit hydrologicUnitCode, found {majorCodeCount}", nameof(hydrologicUnitCodes));
        }

        if (minorCodeCount > 10)
        {
            throw new RequestBuilderException(
                $@"Only allowed 10 minor 8 digit hydrologicUnitCode, found {minorCodeCount}", nameof(hydrologicUnitCodes));
        }

        _hydrologicUnitCodes = hydrologicUnitCodes;
        return this;
    }

    public NwisSiteRequestBuilder SiteNumbers(params string[] siteNumbers)
    {
        if (siteNumbers is null || siteNumbers.Length == 0 || siteNumbers.Any(string.IsNullOrEmpty))
        {
            throw new RequestBuilderException("siteNumbers parameter cannot be empty", nameof(siteNumbers));
        }
        _siteNumbers = siteNumbers;
        return this;
    }

    public NwisSiteRequestBuilder BoundingBox(Envelope boundingBox)
    {
        _boundingBox = boundingBox;
        return this;
    }

    public NwisSiteRequestBuilder SiteOutput(NwisSiteOutput siteOutput)
    {
        _siteOutput = siteOutput;
        return this;
    }

    public NwisSiteRequestBuilder StartDate(DateTime startDate)
    {
        _startDate = startDate;
        return this;
    }

    public NwisSiteRequestBuilder EndDate(DateTime endDate)
    {
        _endDate = endDate;
        return this;
    }

    public NwisSiteRequestBuilder Period(TimeSpan period)
    {
        _period = period;
        return this;
    }

    public NwisSiteRequestBuilder ModifiedSince(TimeSpan modifiedSince)
    {
        _modifiedSince = modifiedSince;
        return this;
    }

    public NwisSiteRequestBuilder SiteName(string siteName)
    {
        _siteName = siteName;
        return this;
    }

    public NwisSiteRequestBuilder SiteNameMatchOperator(NwisSiteNameMatch siteNameMatchOperator)
    {
        _siteNameMatchOperator = siteNameMatchOperator;
        return this;
    }

    public NwisSiteRequestBuilder SiteStatus(NwisSiteStatus siteStatus)
    {
        _siteStatus = siteStatus;
        return this;
    }

    public NwisSiteRequestBuilder SiteTypes(params NwisCode[] siteTypes)
    {
        _siteTypes = siteTypes.ToList();
        return this;
    }

    public NwisSiteRequestBuilder HasDataTypeCode(NwisCode hasDataTypeCode)
    {
        _hasDataTypeCode = hasDataTypeCode;
        return this;
    }

    public NwisSiteRequestBuilder ParameterCode(NwisCode parameterCode)
    {
        _parameterCode = parameterCode;
        return this;
    }

    public NwisSiteRequestBuilder AgencyCode(NwisCode agencyCode)
    {
        _agencyCode = agencyCode;
        return this;
    }

    public NwisSiteRequestBuilder MinimumAltitude(double altMin)
    {
        _altMin = altMin;
        return this;
    }

    public NwisSiteRequestBuilder MaximumAltitude(double altMax)
    {
        _altMax = altMax;
        return this;
    }

    public NwisSiteRequestBuilder MinimumDrainageArea(double drainAreaMin)
    {
        _drainAreaMin = drainAreaMin;
        return this;
    }

    public NwisSiteRequestBuilder MaximumDrainageArea(double drainAreaMax)
    {
        _drainAreaMax = drainAreaMax;
        return this;
    }

    public NwisSiteRequestBuilder AquiferCode(NwisCode aquiferCode)
    {
        _aquiferCode = aquiferCode;
        return this;
    }

    public NwisSiteRequestBuilder LocalAquiferCode(NwisCode localAquiferCode)
    {
        _localAquiferCode = localAquiferCode;
        return this;
    }

    public NwisSiteRequestBuilder MinimumWellDepth(double wellDepthMin)
    {
        _wellDepthMin = wellDepthMin;
        return this;
    }

    public NwisSiteRequestBuilder MaximumWellDepth(double wellDepthMax)
    {
        _wellDepthMax = wellDepthMax;
        return this;
    }

    public NwisSiteRequestBuilder MinimumHoleDepth(double holeDepthMin)
    {
        _holeDepthMin = holeDepthMin;
        return this;
    }

    public NwisSiteRequestBuilder MaximumHoleDepth(double holeDepthMax)
    {
        _holeDepthMax = holeDepthMax;
        return this;
    }

    public NwisSiteRequestBuilder DataCollectionTypeCode(NwisDataCollectionTypeCode dataCollectionTypeCode)
    {
        _dataCollectionTypeCode = dataCollectionTypeCode;
        return this;
    }

    public NwisSiteRequestBuilder SeriesCatalogOutput(bool seriesCatalogOutput)
    {
        _seriesCatalogOutput = seriesCatalogOutput;
        return this;
    }

    public IWaterDataRequest<NwisSite> BuildRequest()
    {
        var fieldDict = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(f => f.GetValue(this) is not null)
            .ToDictionary(
                fi => fi.Name,
                fi => fi.GetCustomAttribute(typeof(NwisQueryParameterAttribute)) as NwisQueryParameterAttribute ?? throw new RequestBuilderException("Nwis Parameters must be annotation with the 'NwisQueryParameter' attribute"));

        var majorParamsCount = fieldDict.Values.Count(f => f.ParameterType == NwisParameterType.Major);
        if (majorParamsCount > 1)
        {
            var agg = fieldDict.Values
                .Where(f => f.ParameterType == NwisParameterType.Major)
                .Select(f => f.Name)
                .ToList();
            throw new RequestBuilderException(
            $@"Only 1 major parameter allowed in NWIS requests,
                    found {majorParamsCount}.Choose 1 of {string.Join(',', agg)}");
        }

        var sb = new StringBuilder();
        if (_countyCodes is not null && _countyCodes.Count > 0)
        {
            sb.Append($"{fieldDict[nameof(_countyCodes)].Name}={string.Join(',', _countyCodes.Select(c => c.Code).ToList().SelectNonEmpty())}");
            sb.Append('&');
        }

        if (_stateCode is not null && !string.IsNullOrEmpty(_stateCode.Code))
        {
            sb.Append($"{fieldDict[nameof(_stateCode)].Name}={string.Join(',', _stateCode.Code)}");
            sb.Append('&');
        }

        if (_hydrologicUnitCodes is not null && _hydrologicUnitCodes.Count > 0)
        {
            sb.Append($"{fieldDict[nameof(_hydrologicUnitCodes)].Name}={string.Join(',', _hydrologicUnitCodes.Select(c => c.Code).ToList().SelectNonEmpty())}");
            sb.Append('&');
        }

        if (_siteNumbers is not null && _siteNumbers.Count > 0)
        {
            sb.Append($"{fieldDict[nameof(_siteNumbers)].Name}={string.Join(',', _siteNumbers.SelectNonEmpty())}");
            sb.Append('&');
        }

        if (_boundingBox is not null && _boundingBox.Area != 0.0)
        {
            sb.Append($"{fieldDict[nameof(_stateCode)].Name}={_boundingBox.MinX},{_boundingBox.MinY},{_boundingBox.MaxX},{_boundingBox.MaxY}");
            sb.Append('&');
        }

        if ((_startDate is not null && _endDate is null) || (_startDate is null && _endDate is not null))
        {
            throw new RequestBuilderException("When startDate or endDate is provided you must also provide the complementary value.");
        }

        if (_startDate is not null)
        {
            sb.Append($"{fieldDict[nameof(_startDate)].Name}={_startDate.Value.Date.ToString("o", CultureInfo.InvariantCulture)}");
            sb.Append('&');
        }

        if (_endDate is not null)
        {
            sb.Append($"{fieldDict[nameof(_endDate)].Name}={_endDate.Value.Date.ToString("o", CultureInfo.InvariantCulture)}");
            sb.Append('&');
        }

        if (_period is not null)
        {
            sb.Append($"{fieldDict[nameof(_period)].Name}={XmlConvert.ToString(_period.Value)}");
            sb.Append('&');
        }

        if (_modifiedSince is not null)
        {
            sb.Append($"{fieldDict[nameof(_modifiedSince)].Name}={XmlConvert.ToString(_modifiedSince.Value)}");
            sb.Append('&');
        }

        if (!string.IsNullOrEmpty(_siteName))
        {
            sb.Append($"{fieldDict[nameof(_siteName)].Name}={_siteName}");
            sb.Append('&');
            if (_siteNameMatchOperator is not null)
            {
                sb.Append($"{fieldDict[nameof(_siteNameMatchOperator)].Name}={_siteNameMatchOperator.Value.GetDescription()}");
                sb.Append('&');
            }
        }

        if (_siteTypes is not null && _siteTypes.Count > 0)
        {
            sb.Append($"{fieldDict[nameof(_siteTypes)].Name}={string.Join(',', _siteTypes.Select(c => c.Code).ToList().SelectNonEmpty())}");
            sb.Append('&');
        }

        if (_hasDataTypeCode is not null && !string.IsNullOrEmpty(_hasDataTypeCode.Code))
        {
            sb.Append($"{fieldDict[nameof(_hasDataTypeCode)].Name}={_hasDataTypeCode.Code}");
            sb.Append('&');
        }

        if (_parameterCode is not null && !string.IsNullOrEmpty(_parameterCode.Code))
        {
            sb.Append($"{fieldDict[nameof(_parameterCode)].Name}={_parameterCode.Code}");
            sb.Append('&');
        }

        if (_agencyCode is not null && !string.IsNullOrEmpty(_agencyCode.Code))
        {
            sb.Append($"{fieldDict[nameof(_agencyCode)].Name}={_agencyCode.Code}");
            sb.Append('&');
        }

        if (_altMin is not null)
        {
            sb.Append($"{fieldDict[nameof(_altMin)].Name}={_altMin}");
            sb.Append('&');
        }

        if (_altMax is not null)
        {
            sb.Append($"{fieldDict[nameof(_altMax)].Name}={_altMax}");
            sb.Append('&');
        }

        if (_drainAreaMin is not null)
        {
            sb.Append($"{fieldDict[nameof(_drainAreaMin)].Name}={_drainAreaMin}");
            sb.Append('&');
        }

        if (_drainAreaMax is not null)
        {
            sb.Append($"{fieldDict[nameof(_drainAreaMax)].Name}={_drainAreaMax}");
            sb.Append('&');
        }

        if (_aquiferCode is not null && !string.IsNullOrEmpty(_aquiferCode.Code))
        {
            sb.Append($"{fieldDict[nameof(_aquiferCode)].Name}={_aquiferCode.Code}");
            sb.Append('&');
        }

        if (_localAquiferCode is not null && !string.IsNullOrEmpty(_localAquiferCode.Code))
        {
            sb.Append($"{fieldDict[nameof(_localAquiferCode)].Name}={_localAquiferCode.Code}");
            sb.Append('&');
        }

        if (_wellDepthMin is not null)
        {
            sb.Append($"{fieldDict[nameof(_wellDepthMin)].Name}={_wellDepthMin}");
            sb.Append('&');
        }

        if (_wellDepthMax is not null)
        {
            sb.Append($"{fieldDict[nameof(_wellDepthMax)].Name}={_wellDepthMax}");
            sb.Append('&');
        }

        if (_holeDepthMin is not null)
        {
            sb.Append($"{fieldDict[nameof(_holeDepthMin)].Name}={_holeDepthMin}");
            sb.Append('&');
        }

        if (_holeDepthMax is not null)
        {
            sb.Append($"{fieldDict[nameof(_holeDepthMax)].Name}={_holeDepthMax}");
            sb.Append('&');
        }

        if (_dataCollectionTypeCode is not null)
        {
            sb.Append($"{fieldDict[nameof(_dataCollectionTypeCode)].Name}={_dataCollectionTypeCode.Code}");
            sb.Append('&');
        }

        sb.Append($"{fieldDict[nameof(_siteStatus)].Name}={_siteStatus.GetDescription()}");
        sb.Append('&');
        sb.Append($"{fieldDict[nameof(_siteOutput)].Name}={_siteOutput.GetDescription()}");
        sb.Append('&');
        sb.Append($"{fieldDict[nameof(_format)].Name}={_format}");
        sb.Append('&');
        sb.Append($"{fieldDict[nameof(_seriesCatalogOutput)].Name}={_seriesCatalogOutput.ToString().ToLowerInvariant()}");

        var builder = new UriBuilder($"{NwisRequestBuilder.ApiUrl}{UrlPath}")
        {
            Query = sb.ToString()
        };

        return new NwisHttpRequest<NwisSite>(builder.Uri);
    }
}
