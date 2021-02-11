using System;

namespace Lendmatic.HotUpdatePlugin.Objects
{
    public class ImplementationException: Exception
    {
        public ImplementationException(string EventType, string EventName, string Interface)
        : base($"{EventType} Must Implement {EventName} for {Interface}")
        { }
    }
}
