using System.Reflection;

namespace OpenTransit.Engine.Dependencies;

public static class DependencyInjector
{
    public static void Inject(object target, DependencyContainer dependencies)
    {
        var type = target.GetType();

        while (type != null && type != typeof(object))
        {
            // Inject into properties
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly))
            {
                var attr = prop.GetCustomAttribute<ResolvedAttribute>();
                if (attr != null)
                {
                    var getMethod = prop.GetGetMethod(true);
                    var setMethod = prop.GetSetMethod(true);

                    if ((getMethod != null && !getMethod.IsPrivate) || (setMethod != null && !setMethod.IsPrivate))
                    {
                        throw new Exception(
                            $"[Resolved] can only be used on private properties. Property: {type.Name}.{prop.Name}");
                    }

                    if (prop.CanWrite)
                    {
                        var val = dependencies.Get(prop.PropertyType, attr.Name);
                        if (val == null && !attr.CanBeNull)
                        {
                            throw new Exception(
                                $"Could not resolve dependency {prop.PropertyType.Name} for {type.Name}.{prop.Name}");
                        }

                        if (val != null)
                            prop.SetValue(target, val);
                    }
                }
            }

            type = type.BaseType;
        }
    }

    /// <summary>
    /// Creates a new container that wraps the parent, and caches any [Cached] members of the target.
    /// Returns the parent if no caching is needed.
    /// </summary>
    public static DependencyContainer RegisterCached(object target, DependencyContainer parent)
    {
        var type = target.GetType();

        bool hasCached = type.GetCustomAttribute<CachedAttribute>() != null;
        if (!hasCached)
        {
             // Check members
             foreach (var member in type.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
             {
                 if (member.GetCustomAttribute<CachedAttribute>() != null)
                 {
                     hasCached = true;
                     break;
                 }
             }
        }

        if (!hasCached)
            return parent;

        var container = new DependencyContainer(parent);

        // Class attribute
        var classCached = type.GetCustomAttribute<CachedAttribute>();
        if (classCached != null)
        {
            container.Cache(target, classCached.Type, classCached.Name);
        }

        // Fields
        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            var attr = field.GetCustomAttribute<CachedAttribute>();
            if (attr != null)
            {
                var val = field.GetValue(target);
                if (val != null)
                    container.Cache(val, attr.Type ?? field.FieldType, attr.Name);
            }
        }

        // Properties
        foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            var attr = prop.GetCustomAttribute<CachedAttribute>();
            if (attr != null)
            {
                var val = prop.GetValue(target);
                if (val != null)
                     container.Cache(val, attr.Type ?? prop.PropertyType, attr.Name);
            }
        }

        return container;
    }
}
