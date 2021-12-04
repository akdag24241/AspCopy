using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.DependencyInjection
{
    public enum Lifetime
    {
        Scoped,
        Transient,
        Singleton
    }

    public class Dependency
    {
        public string ScopeGuid { get; set; }
        public Lifetime Lifetime { get; set; }
        public Type InterfaceType { get; set; }
        public Type ConcreteType { get; set; }
        public object Instance { get; set; }

        public Dependency(Type interfaceType, Type concreteType, Lifetime lifetime)
        {
            Lifetime = lifetime;
            InterfaceType = interfaceType;
            ConcreteType = concreteType;
        }

        public Dependency(Type concreteType, Lifetime lifetime)
        {
            Lifetime = lifetime;
            ConcreteType = concreteType;
        }

        public Dependency(object instance, Type type, Lifetime lifetime)
        {
            Lifetime = lifetime;
            Instance = instance;
            ConcreteType = type;
            InterfaceType = type;
        }

    }

    public interface IDIContainer
    {
        T Get<T>();
        object Get(Type t);

        void AddTransient(Type t);
        void AddTransient(object o);
        void AddTransient<T, R>();
        void AddTransient<T>(object o);
        void AddTransient<T>();


        void AddScoped(Type t);
        void AddScoped(object o);
        void AddScoped<T, R>();
        void AddScoped<T>(object o);
        void AddScoped<T>();


        void AddSingleton(Type t);
        void AddSingleton(object o);
        void AddSingleton<T, R>();
        void AddSingleton<T>(object o);
        void AddSingleton<T>();

        void ResetScopedInstances();
        void SetScopedGuid();
    }

    public class DIContainer : IDIContainer
    {
        private string _currentScope = null;
        private List<Dependency> _dependencies { get; set; }

        public DIContainer()
        {
            _dependencies = new List<Dependency>();
        }

        #region Add Transient Overrides

        public void AddTransient(Type t)
        {
            _dependencies.Add(new Dependency(t, Lifetime.Transient));
        }

        public void AddTransient(object o)
        {
            _dependencies.Add(new Dependency(o, o.GetType(), Lifetime.Transient));
        }

        public void AddTransient<Interface, Concrete>()
        {
            _dependencies.Add(new Dependency(typeof(Interface), typeof(Concrete), Lifetime.Transient));
        }

        public void AddTransient<T>(object o)
        {
            _dependencies.Add(new Dependency(o, typeof(T), Lifetime.Transient));
        }

        public void AddTransient<Concrete>()
        {
            _dependencies.Add(new Dependency(typeof(Concrete), Lifetime.Transient));
        }

        #endregion

        #region Add Scoped Overrides
        public void AddScoped(Type t)
        {
            _dependencies.Add(new Dependency(t, Lifetime.Scoped));
        }

        public void AddScoped(object o)
        {
            _dependencies.Add(new Dependency(o, o.GetType(), Lifetime.Scoped));
        }

        public void AddScoped<Interface, Concrete>()
        {
            _dependencies.Add(new Dependency(typeof(Interface), typeof(Concrete), Lifetime.Scoped));
        }

        public void AddScoped<T>(object o)
        {
            _dependencies.Add(new Dependency(o, typeof(T), Lifetime.Scoped));
        }

        public void AddScoped<Concrete>()
        {
            _dependencies.Add(new Dependency(typeof(Concrete), Lifetime.Scoped));
        }
        #endregion

        #region Add Singleton Overrides

        public void AddSingleton(Type t)
        {
            _dependencies.Add(new Dependency(t, Lifetime.Singleton));
        }

        public void AddSingleton(object o)
        {
            _dependencies.Add(new Dependency(o, o.GetType(), Lifetime.Singleton));
        }

        public void AddSingleton<Interface, Concrete>()
        {
            _dependencies.Add(new Dependency(typeof(Interface), typeof(Concrete), Lifetime.Singleton));
        }

        public void AddSingleton<T>(object o)
        {
            _dependencies.Add(new Dependency(o, typeof(T), Lifetime.Singleton));
        }

        public void AddSingleton<Concrete>()
        {
            _dependencies.Add(new Dependency(typeof(Concrete), Lifetime.Singleton));
        }

        #endregion

        public Type Get<Type>()
        {
            return (Type)CreateInstance(typeof(Type));
        }

        public object Get(Type t)
        {
            return CreateInstance(t);
        }

        private object CreateInstance(Type t)
        {
            var typeOfDependency = _dependencies.Find(x => x.ConcreteType == t || x.InterfaceType == t);

            if (typeOfDependency == null)
            {
                throw new InvalidDataException();
            }

            if (typeOfDependency.Instance != null && ((typeOfDependency.Lifetime == Lifetime.Scoped && typeOfDependency.ScopeGuid == _currentScope) ||
                                                       typeOfDependency.Lifetime == Lifetime.Singleton))

            {
                return typeOfDependency.Instance;
            }

            var constuctors = typeOfDependency.ConcreteType.GetConstructors().ToList();
            if (constuctors.Count > 1)
            {
                throw new InvalidDataException();
            }

            if (constuctors.Count == 1)
            {
                var constructor = constuctors.First();
                var parameters = constructor.GetParameters().ToList();
                var instanceList = new List<object>();
                foreach (var param in parameters)
                {
                    var instance = CreateInstance(param.ParameterType);
                    instanceList.Add(instance);
                }
                typeOfDependency.Instance = Activator.CreateInstance(typeOfDependency.ConcreteType, instanceList.ToArray());
                return typeOfDependency.Instance;
            }

            typeOfDependency.Instance = Activator.CreateInstance(typeOfDependency.ConcreteType);
            return typeOfDependency.Instance;
        }

        private IEnumerable<Dependency> GetScopedDependencies() => _dependencies.Where(x => x.Lifetime == Lifetime.Scoped);

        public void ResetScopedInstances()
        {
            var depList = GetScopedDependencies().Where(x => x.ScopeGuid == _currentScope);
            foreach (var dep in depList)
            {
                dep.Instance = null;
            }
        }

        public void SetScopedGuid()
        {
            var depList = GetScopedDependencies();
            var guid = Guid.NewGuid().ToString();
            _currentScope = guid;
            foreach (var dep in depList)
            {
                dep.ScopeGuid = guid;
            }
        }

    }
}
