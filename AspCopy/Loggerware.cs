using AspCopy.Context;
using AspCopy.Middlewares.Builder;
using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy
{
    public class Loggerware : ServiceMethod
    {
        public Loggerware(IUserDatabase userDatabase)
        {

        }



        public override async Task Execute(DataContext dataContext)
        {

            //requestten işletiliyor
            await _next.Execute(dataContext);
            // response işletiyo
        }
    }

}
