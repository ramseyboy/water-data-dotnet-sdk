namespace WaterData.Nwis;

public class NwisQueryParameterAttribute : Attribute
{
    public NwisQueryParameterAttribute(string name, NwisParameterType parameterType)
    {
        Name = name;
        ParameterType = parameterType;
    }

    public string Name { get; set; }

    public NwisParameterType ParameterType { get; set; }
}
