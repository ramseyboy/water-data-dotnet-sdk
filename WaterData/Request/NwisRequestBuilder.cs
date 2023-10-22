using WaterData.Exceptions;
using WaterData.Models.Codes;
using WaterData.Request.Codes;
using WaterData.Request.Site;

namespace WaterData.Request;

public class NwisRequestBuilder: IWaterDataRequestBuilder<object>
{
    public const string ApiUrl = "https://waterservices.usgs.gov/nwis";

    public static NwisRequestBuilder Builder()
    {
        return new NwisRequestBuilder();
    }

    private NwisRequestBuilder()
    {
    }

    public NwisSiteRequestBuilder Sites()
    {
        return new NwisSiteRequestBuilder();
    }

    public NwisCodesRequestBuilder<NwisCountyCode> CountyCodes()
    {
        const string fileName = "county_codes.tsv";
        return new NwisCodesRequestBuilder<NwisCountyCode>(fileName);
    }

    public NwisCodesRequestBuilder<NwisStateCode> StateCodes()
    {
        const string fileName = "state_codes.tsv";
        return new NwisCodesRequestBuilder<NwisStateCode>(fileName);
    }

    public NwisCodesRequestBuilder<NwisHydrologicUnitCodes> HydrologicUnitCodes()
    {
        const string fileName = "hydrologic_unit_codes.tsv";
        return new NwisCodesRequestBuilder<NwisHydrologicUnitCodes>(fileName);
    }

    public NwisCodesRequestBuilder<NwisAgencyCode> AgencyCodes()
    {
        const string fileName = "agency_cd_query.tsv";
        return new NwisCodesRequestBuilder<NwisAgencyCode>(fileName);
    }

    public NwisParameterCodesRequestBuilder ParameterCodes()
    {
        return new NwisParameterCodesRequestBuilder();
    }

    public NwisCodesRequestBuilder<NwisAquiferCode> AquiferCodes()
    {
        const string fileName = "nat_aqfr_query.tsv";
        return new NwisCodesRequestBuilder<NwisAquiferCode>(fileName);
    }

    public NwisCodesRequestBuilder<NwisLocalAquiferCode> LocalAquiferCodes()
    {
        const string fileName = "aqfr_cd_query.tsv";
        return new NwisCodesRequestBuilder<NwisLocalAquiferCode>(fileName);
    }

    public NwisCodesRequestBuilder<NwisDataCollectionTypeCode> DataCollectionTypeCodes()
    {
        //TODO: find codes
        const string fileName = "data_collection_cd.tsv";
        return new NwisCodesRequestBuilder<NwisDataCollectionTypeCode>(fileName);
    }

    public NwisCodesRequestBuilder<NwisSiteTypeCode> SiteTypeCodes()
    {
        const string fileName = "site_tp_query.tsv";
        return new NwisCodesRequestBuilder<NwisSiteTypeCode>(fileName);
    }

    public IWaterDataRequest<object> BuildRequest()
    {
        throw new RequestBuilderException("Invalid builder, call one of the builder methods to continue building an NWIS request.");
    }
}
