using System;
using System.Runtime.Serialization;

namespace Build.Exceptions;

[Serializable]
public class XUnitResourceNullException
    : Exception
{
    public XUnitResourceNullException()
    {
    }

    public XUnitResourceNullException(string message)
        : base(message)
    {
    }

    public XUnitResourceNullException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected XUnitResourceNullException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}
