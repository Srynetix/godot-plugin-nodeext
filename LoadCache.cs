using Godot;
using Godot.Collections;

namespace SxGD
{
    public class LoadCache : Node
    {
        public Dictionary<string, object> _Cache = new Dictionary<string, object>();
        private static LoadCache _GlobalInstance;

        public LoadCache()
        {
            if (_GlobalInstance == null)
            {
                _GlobalInstance = this;
            }

            Initialize();
        }

        // Inherit this method to load resources.
        public virtual void Initialize()
        {

        }

        public static LoadCache GetInstance()
        {
            return _GlobalInstance;
        }

        public void StoreScene(string name, string path)
        {
            var scene = GD.Load<PackedScene>(path);
            _Cache[name] = scene;
        }

        public void StoreResource<T>(string name, string path) where T : Resource
        {
            _Cache[name] = GD.Load<T>(path);
        }

        public bool HasScene(string name)
        {
            return _Cache.ContainsKey(name);
        }

        public bool HasScene<T>() where T : class
        {
            return HasScene(typeof(T).Name);
        }

        public void StoreScene<T>(string path) where T : class
        {
            var name = typeof(T).Name;
            StoreScene(name, path);
        }

        public PackedScene LoadScene(string name)
        {
            return (PackedScene)_Cache[name];
        }

        public PackedScene LoadScene<T>() where T : class
        {
            var name = typeof(T).Name;
            return LoadScene(name);
        }

        public Resource LoadResource(string name)
        {
            return (Resource)_Cache[name];
        }

        public T LoadResource<T>(string name)
        {
            return (T)_Cache[name];
        }

        public T InstantiateScene<T>(string name) where T : class
        {
            return LoadScene(name).Instance<T>();
        }

        public T InstantiateScene<T>() where T : class
        {
            return LoadScene<T>().Instance<T>();
        }
    }
}
