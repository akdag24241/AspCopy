using AspCopy.Context;
using AspCopy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.Middlewares.Internal
{
    public class RequestRetriever : ServiceMethod
    {
        private HttpListener _listener;
        private IDIContainer _container;
        public RequestRetriever(IDIContainer container)
        {
            _container = container;
        }

        public override async Task Execute(DataContext dataContext)
        {
            Console.WriteLine("RequestRetriever BEFORE");

            
           
            await _next.Execute(dataContext);


        }


        
    }
}
