using AspCopy.Context;
using AspCopy.DependencyInjection;
using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.Middlewares.Base
{
    public class ResponseRetriever : ServiceMethod
    {
        private IDIContainer _diContainer;
        public ResponseRetriever(IDIContainer container)
        {
            _diContainer = container;
        }
        public override async Task Execute(DataContext dataContext)
        {


            var controllerType = dataContext.ControllerInfo.GetControllerType();
            var controllerCtor = dataContext.ControllerInfo.GetConstructor();
            var controllerParams = dataContext.ControllerInfo.GetConstructorParameterTypes();

            var instanceList = new List<object>();

            foreach(var controllerParam in controllerParams)
            {
                instanceList.Add(_diContainer.Get(controllerParam));
            }

            var controllerInstance = Activator.CreateInstance(controllerType, instanceList.ToArray());

            var methodInfo = controllerInstance.GetType().GetMethod("InsertUser");
            var result = methodInfo.Invoke(controllerInstance, new object[] { dataContext.Request });


            await _next.Execute(dataContext);
        }
    }
}
