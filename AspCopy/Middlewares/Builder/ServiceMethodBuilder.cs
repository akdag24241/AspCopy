using AspCopy.Context;
using AspCopy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.Middlewares.Builder
{
    public class ServiceMethodBuilder
    {
        private ServiceMethod _first;
        private ServiceMethod _instanceMethod;
        private IDIContainer _diContainer;

        public ServiceMethodBuilder(IDIContainer diContainer)
        {
            _diContainer = diContainer;
            _diContainer.Add<IDIContainer>(diContainer);
        }

        public void Add<T>() where T : ServiceMethod
        {
            _diContainer.Add<T>();
            if (_first == null)
            {   
                _first = _diContainer.Get<T>();
                _first._next = new EmptyServiceMethod();
                _instanceMethod = _first;
                return;
            }
            _instanceMethod._next = _diContainer.Get<T>();
            _instanceMethod = _instanceMethod._next;
            _instanceMethod._next = new EmptyServiceMethod();
        }

        public async Task Run()
        {
            while (true)
            {
                await _first.Execute(new DataContext());
            }
        }
    }
}
