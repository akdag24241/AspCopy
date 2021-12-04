using AspCopy.Context;
using AspCopy.Middlewares.Builder;
using AspCopy.Middlewares.Internal;
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
        private ILogger _logger;
        public Loggerware(ILogger logger)
        {
            _logger = logger;
        }



        public override async Task Execute(DataContext dataContext)
        {

            _logger.AddLog("BEFORE Loggerware");
            await _next.Execute(dataContext);
            _logger.AddLog("AFTER Loggerware");
        }
    }

}
