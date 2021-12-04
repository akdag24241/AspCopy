using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.DependencyInjection
{
    public interface IExample
    {
        void Execute();
    }
    public class ExampleDependency : IExample
    {
        public void Execute()
        {
            Console.WriteLine("Im Executing Something");
        }
    }
}
