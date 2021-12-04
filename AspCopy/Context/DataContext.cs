using AspCopy.Middlewares.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.Context
{
    public class DataContext
    {
        public DataContext(HttpListenerRequest httpListenerRequest, HttpListenerResponse httpListenerResponse) 
        {
            ListenerRequest = httpListenerRequest;
            ListenerResponse = httpListenerResponse;
        }
        public HttpListenerRequest ListenerRequest { get; set; }
        public HttpListenerResponse ListenerResponse { get; set; }
    }
}
