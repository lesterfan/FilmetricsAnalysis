using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmetricsAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            MicroscopeAnalyzer mAnalyzer = new MicroscopeAnalyzer();

            mAnalyzer.AcquireBaseline();
            if (mAnalyzer.mLastRet == 1) Console.WriteLine("Error!");
        }
    }
}
