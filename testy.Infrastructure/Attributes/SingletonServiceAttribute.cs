using System;

namespace testy.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonServiceAttribute : Attribute
    {
    }
}
