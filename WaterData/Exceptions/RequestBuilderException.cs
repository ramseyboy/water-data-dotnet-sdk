using System.Runtime.Serialization;

namespace WaterData.Exceptions;

[Serializable]
public class RequestBuilderException: ArgumentException
{
    public RequestBuilderException()
    {
    }

    protected RequestBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public RequestBuilderException(string? message) : base(message)
    {
    }

    public RequestBuilderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public RequestBuilderException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public RequestBuilderException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}
