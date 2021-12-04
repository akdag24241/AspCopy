using AspCopy.Context;
using AspCopy.Middlewares.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy
{
    public class Loggerware : ServiceMethod
    {
        

       

        public override async Task Execute(DataContext dataContext)
        {
            
            
            await _next.Execute(dataContext);
            
        }
    }

}
