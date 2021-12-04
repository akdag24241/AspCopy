using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCopy.DependencyInjection
{
    public interface ITest
    {
        public void Run();
    }
    public class TestDependency : ITest
    {
        IExample Example;
        public TestDependency(IExample example1) { Example = example1; }
        public void Run()
        {
            Example.Execute();
        }
    }
}
