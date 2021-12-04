using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.Middlewares.Internal
{
    public interface ILogger
    {
        void AddLog(string message);
    }

    public class Logger : ILogger
    {
        public void AddLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}
