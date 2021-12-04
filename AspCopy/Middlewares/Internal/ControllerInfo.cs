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
        MethodInfo GetMethod();
        ConstructorInfo GetConstructor();
        Type GetControllerType();
        Type[] GetConstructorParameterTypes();
    }

    public class ControllerInfo : IControllerInfo
    {
        private Type _controllerType;
        private string methodName;
        public ControllerInfo(Uri uri)
        {
            string controllerName = uri.Segments[1].Replace("/", "");
            methodName = uri.Segments[2];
            var result = Assembly.GetAssembly(typeof(BaseController)).GetTypes().Where(x => x.Name == controllerName + "Controller").FirstOrDefault();
            
            _controllerType = result;
        }

        

        public ConstructorInfo GetConstructor()
        {
            var constructors = _controllerType.GetConstructors();
            return constructors.First();
        }

        public MethodInfo GetMethod()
        {
            return _controllerType.GetMethod(methodName);
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
}
