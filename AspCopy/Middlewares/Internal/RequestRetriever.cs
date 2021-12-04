using AspCopy.Context;
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
        public RequestRetriever(HttpListener listener)
        {
            _listener = listener;
        }

        public override async Task Execute(DataContext dataContext)
        {
            Console.WriteLine("RequestRetriever BEFORE");
            var context = await _listener.GetContextAsync();
            var requestContext = context.Request;
            string text;
            using (var reader = new StreamReader(requestContext.InputStream,
                                                 requestContext.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }
            var createdContext = new DataContext()
            {
                Request = text
            };
            
            await _next.Execute(createdContext);
            Console.WriteLine("RequestRetriever AFTER");
        }


        
    }
}
