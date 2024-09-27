using System;

namespace ROFE.Application;

[Serializable]
public class UseCaseException : Exception
{
    public UseCaseException() { }
    public UseCaseException(string message) : base(message) { }
    public UseCaseException(string message, Exception inner) : base(message, inner) { }
}
