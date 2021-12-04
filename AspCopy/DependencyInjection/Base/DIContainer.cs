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
        public Lifetime Lifetime { get; set; }  

        public Type InterfaceType { get; set; }
        public Type ConcreteType { get; set; }
        public object Instance { get; set; }

        public Dependency(Type interfaceType, Type concreteType)
        {
            InterfaceType = interfaceType;
            ConcreteType = concreteType;
        }

        public Dependency(Type concreteType)
        {
            ConcreteType = concreteType;
        }

        public Dependency(object instance, Type type)
        {
            Instance = instance;
            ConcreteType = type;
            InterfaceType = type;
        }

    }

    public interface IDIContainer
    {
        T Get<T>();
        object Get(Type t);
        void Add(Type t);
        void Add(object o);
        void Add<T, R>();
        void Add<T>(object o);
        void Add<T>();
    }

    public class DIContainer : IDIContainer
    {

        private List<Dependency> _dependencies { get; set; }

        public DIContainer()
        {
            _dependencies = new List<Dependency>();
        }

        public void Add(Type t)
        {
            _dependencies.Add(new Dependency(t));
        }

        public void Add(object o)
        {
            _dependencies.Add(new Dependency(o, o.GetType()));
        }

        public void Add<Interface, Concrete>()
        {
            _dependencies.Add(new Dependency(typeof(Interface), typeof(Concrete)));
        }

        public void Add<T>(object o)
        {
            _dependencies.Add(new Dependency(o, typeof(T)));
        }

        public void Add<Concrete>()
        {
            _dependencies.Add(new Dependency(typeof(Concrete)));
        }

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

            if(typeOfDependency.Instance != null)
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
                return Activator.CreateInstance(typeOfDependency.ConcreteType, instanceList.ToArray());
            }

            return Activator.CreateInstance(typeOfDependency.ConcreteType);
        }



        
    }
}
