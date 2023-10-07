namespace WaterData.Parameters;

public interface IParametersBuilder
{
    public string ApiUrl { get; }

    public NwisParameters BuildParameters();
}
