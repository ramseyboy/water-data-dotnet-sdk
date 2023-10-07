using System.Runtime.Serialization;

namespace WaterData.Exceptions;

[Serializable]
public class ParameterException: ArgumentException
{
    public ParameterException()
    {
    }

    protected ParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ParameterException(string? message) : base(message)
    {
    }

    public ParameterException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public ParameterException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public ParameterException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}