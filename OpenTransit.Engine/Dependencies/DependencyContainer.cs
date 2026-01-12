
namespace OpenTransit.Engine.Dependencies;

public class DependencyContainer
{
    private readonly Dictionary<(Type, string?), object> _cache = new();
    private readonly DependencyContainer? _parent;

    public DependencyContainer(DependencyContainer? parent = null)
    {
        _parent = parent;
    }

    public void Cache(object instance, Type? asType = null, string? name = null)
    {
        var type = asType ?? instance.GetType();
        _cache[(type, name)] = instance;
    }

    public void Cache<T>(T instance, string? name = null) where T : class
    {
         Cache(instance, typeof(T), name);
    }

    public object? Get(Type type, string? name = null)
    {
        if (_cache.TryGetValue((type, name), out var instance))
        {
            return instance;
        }

        return _parent?.Get(type, name);
    }

    public T? Get<T>(string? name = null) where T : class
    {
        return (T?)Get(typeof(T), name);
    }
}

