using System;

namespace testy.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TransientServiceAttribute : Attribute
    {
    }
}
