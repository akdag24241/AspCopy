using AspCopy.Context;
using AspCopy.DependencyInjection;
using AspCopy.Middlewares.Internal;
using Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.Middlewares.Base
{
    public class ResponseRetriever : ServiceMethod
    {
        private IDIContainer _diContainer;
        private IUserDatabase _userDatabase;
        public ResponseRetriever(IDIContainer container, IUserDatabase userDatabase)
        {
            _diContainer = container;
            _userDatabase = userDatabase;
        }
        public override async Task Execute(DataContext dataContext)
        {
            var controllerInfo = new ControllerInfo(dataContext.ListenerRequest.Url);
            var controllerType = controllerInfo.GetControllerType();
            var controllerParams = controllerInfo.GetConstructorParameterTypes();
            var instanceList = new List<object>();
            foreach(var controllerParam in controllerParams)
            {
                instanceList.Add(_diContainer.Get(controllerParam));
            }
            var controllerInstance = Activator.CreateInstance(controllerType, instanceList.ToArray());
            var methodInfo = controllerInfo.GetMethod();
            var methodParameters = methodInfo.GetParameters().FirstOrDefault();

            string text;
            using (var reader = new StreamReader(dataContext.ListenerRequest.InputStream,
                                                 dataContext.ListenerRequest.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }

            var requestObject = JsonConvert.DeserializeObject(text, methodParameters.ParameterType);

            var result = methodInfo.Invoke(controllerInstance, new object[] { requestObject });


            

            using (HttpListenerResponse response = dataContext.ListenerResponse)
            {
                // returning some test results

                // TODO: return the results in JSON format
                response.ContentType = "application/json";
                string responseString = JsonConvert.SerializeObject(result);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                using (var output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
            }
            await _next.Execute(dataContext);
        }

        
        
    }
}
