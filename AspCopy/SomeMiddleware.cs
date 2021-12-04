using AspCopy.Context;
using AspCopy.DependencyInjection;
using AspCopy.Middlewares.Internal;
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
        ILogger _logger;
        public SomeMiddleware(DepChain depChain, ILogger logger)
        {
            _dc = depChain;
            _logger = logger;
        }

        public override async Task Execute(DataContext dataContext)
        {
            _logger.AddLog("BEFORE SomeMiddleware");
            await _next.Execute(dataContext);
            _logger.AddLog("AFTER SomeMiddleware");
        }
    }
}
