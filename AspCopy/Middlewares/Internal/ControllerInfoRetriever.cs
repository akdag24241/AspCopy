using AspCopy.Context;
using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.Middlewares.Internal
{
    public interface IControllerInfo
    {
        Type GetRequestType();
        Type GetResponseType();
        MethodInfo[] GetMethods();
        ConstructorInfo GetConstructor();
        Type GetControllerType();
        Type[] GetConstructorParameterTypes();
    }

    public class ControllerInfo : IControllerInfo
    {
        private Assembly _controllerAssembly;
        private Type _controllerType;
        public ControllerInfo(string url)
        {
            _controllerAssembly = Assembly.GetAssembly(typeof(SomeController));
            _controllerType = typeof(SomeController);
        }

        public ConstructorInfo GetConstructor()
        {
            var constructors = _controllerType.GetConstructors();
            return constructors.First();
        }

        public MethodInfo[] GetMethods()
        {
            return _controllerType.GetMethods();
        }

        public Type GetRequestType()
        {
            return typeof(RequestClass);
        }

        public Type GetResponseType()
        {
            return typeof(RequestClass);
        }

        public Type GetControllerType()
        {
            return _controllerType;
        }

        public Type[] GetConstructorParameterTypes()
        {
            return GetConstructor().GetParameters().Select(x => x.ParameterType).ToArray();
        }
    }

    public class ControllerInfoRetriever : ServiceMethod
    {

        public override async Task Execute(DataContext dataContext)
        {
            dataContext.ControllerInfo = new ControllerInfo(dataContext.Url);


            await _next.Execute(dataContext);
        }
    }
}
