using AspCopy.Context;
using AspCopy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
            _diContainer.AddSingleton<IDIContainer>(diContainer);
        }

        public void Add<T>() where T : ServiceMethod
        {
            _diContainer.AddSingleton<T>();
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
            var listener = _diContainer.Get<HttpListener>();
            while (true)
            {
                var context = await listener.GetContextAsync();

                try
                {
                    _diContainer.SetScopedGuid();
                    await _first.Execute(new DataContext(context.Request, context.Response));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    _diContainer.ResetScopedInstances();
                }

            }
        }
    }
}
