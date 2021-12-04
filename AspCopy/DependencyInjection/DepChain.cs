using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.DependencyInjection
{
    public class DepChain
    {
        DepChain2 _dc2;
        DepChain3 _dc3;
        DepChain4 _dc4;
        public DepChain(DepChain2 dc2, DepChain3 dc3, DepChain4 dc4)
        {
            _dc2 = dc2;
            _dc4 = dc4;
            _dc3 = dc3;
        }

        public void Run()
        {
            Console.WriteLine($"All the dependencies are up {_dc2.GetType()} {_dc3.GetType()} {_dc4.GetType()}");
        }
    }
    public class DepChain2
    {
        public DepChain2(DepChain3 dc3, DepChain4 dc4)
        {
            dc3.Run();
            dc4.Run();
        }

        public void Run() => Console.WriteLine("DepChain2");
    }

    public class DepChain3
    {
        public DepChain3(DepChain4 dc4)
        {
            dc4.Run();
        }
        public void Run() => Console.WriteLine("DepChain3");
    }

    public class DepChain4
    {
        public void Run() => Console.WriteLine("DepChain4");
    }
}
