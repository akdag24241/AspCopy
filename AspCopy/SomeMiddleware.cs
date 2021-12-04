using AspCopy.Context;
using AspCopy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy
{
    public class SomeMiddleware : ServiceMethod
    {
        DepChain _dc;
        public SomeMiddleware(DepChain depChain)
        {
            _dc = depChain;
        }

        public override async Task Execute(DataContext dataContext)
        {
            
            await _next.Execute(dataContext);
            
        }
    }
}
