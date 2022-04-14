using System;
using System.Runtime.Serialization;

namespace Build.Exceptions;

[Serializable]
public class CakeIssuesMsBuildNotFoundException
    : Exception
{
    public CakeIssuesMsBuildNotFoundException()
    {
    }

    public CakeIssuesMsBuildNotFoundException(string message)
        : base(message)
    {
    }

    public CakeIssuesMsBuildNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected CakeIssuesMsBuildNotFoundException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}
