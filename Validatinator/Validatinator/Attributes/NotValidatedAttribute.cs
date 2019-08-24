using System;

namespace Validatinator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotValidatedAttribute : Attribute
    {
    }
}