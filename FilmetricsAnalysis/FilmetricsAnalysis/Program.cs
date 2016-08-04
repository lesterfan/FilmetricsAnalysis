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

            while(true)
            {
                Console.WriteLine("Enter in 'a' to take measurement, enter in 's' to save.");
                string input = Console.ReadLine();
                if (input == "a")
                {
                    Console.WriteLine("Now measuring!");
                    mAnalyzer.Measure();
                    if (mAnalyzer.mLastRet == 1) Console.WriteLine("Something went wrong in the last step!");
                }
                else if (input == "s")                                // Current file directory : C:\ProgramData\Filmetrics\Material
                {
                    Console.WriteLine("Please enter in the name of the file you want it to be saved in.");
                    Console.WriteLine("The program will automatically save it as a '.fmspe' file. ");
                    Console.WriteLine("For now, all files will be saved in directory 'C:/ProgramData/Filmetrics/Material/' ");
                    string currFileDir = "C:/ProgramData/Filmetrics/Material/";
                    string user_input = Console.ReadLine();
                   

                    mAnalyzer.SaveSpectrum(currFileDir, user_input);
                    if (mAnalyzer.mLastRet == 1) Console.WriteLine("Something went wrong in the last step!");
                }

            }
        }
    }
}
