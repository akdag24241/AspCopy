using AspCopy.Middlewares.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.Context
{
    public class DataContext
    {
        public DataContext() { }
        public string Request { get; set; }
        public string Url { get; set; }
        public string Response { get; set; }
        public IControllerInfo ControllerInfo { get; set; }
    }
}
