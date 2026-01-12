using System;

namespace OpenTransit.Engine.Dependencies;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ResolvedAttribute : Attribute
{
    public string? Name { get; }
    public bool CanBeNull { get; }

    public ResolvedAttribute(string? name = null, bool canBeNull = false)
    {
        Name = name;
        CanBeNull = canBeNull;
    }
}

