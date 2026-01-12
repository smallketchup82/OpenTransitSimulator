using System;

namespace OpenTransit.Engine.Dependencies;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class CachedAttribute : Attribute
{
    public Type? Type { get; }
    public string? Name { get; }

    public CachedAttribute(Type? type = null, string? name = null)
    {
        Type = type;
        Name = name;
    }
}

