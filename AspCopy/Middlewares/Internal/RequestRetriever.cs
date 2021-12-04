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
        private ILogger _logger;
        public RequestRetriever(IDIContainer container, ILogger logger)
        {
            _container = container;
            _logger = logger;
        }

        public override async Task Execute(DataContext dataContext)
        {
            _logger.AddLog("BEFORE RequestRetriever");

            
           
            await _next.Execute(dataContext);



            _logger.AddLog("AFTER RequestRetriever");
        }


        
    }
}
