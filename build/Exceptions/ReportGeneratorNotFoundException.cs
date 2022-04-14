using System;
using System.Runtime.Serialization;

namespace Build.Exceptions;

[Serializable]
public class ReportGeneratorNotFoundException
    : Exception
{
    public ReportGeneratorNotFoundException()
    {
    }

    public ReportGeneratorNotFoundException(string message)
        : base(message)
    {
    }

    public ReportGeneratorNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected ReportGeneratorNotFoundException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}
