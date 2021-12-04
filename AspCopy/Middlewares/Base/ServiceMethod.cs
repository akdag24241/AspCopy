using AspCopy.Context;
using AspCopy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy
{
    public abstract class ServiceMethod
    {
        public ServiceMethod _next { get; set; }

        public abstract Task Execute(DataContext dataContext);
    }

    public class EmptyServiceMethod : ServiceMethod
    {


        public override Task Execute(DataContext dataContext)
        {
            
            return Task.CompletedTask;
        }
    }


}
