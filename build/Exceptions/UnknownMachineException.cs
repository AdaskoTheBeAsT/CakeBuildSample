using System;
using System.Runtime.Serialization;

namespace Build.Exceptions;

[Serializable]
public class UnknownMachineException
    : Exception
{
    public UnknownMachineException()
    {
    }

    public UnknownMachineException(string message)
        : base(message)
    {
    }

    public UnknownMachineException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected UnknownMachineException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}
