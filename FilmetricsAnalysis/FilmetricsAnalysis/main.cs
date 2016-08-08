using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace FilmetricsAnalysis
{
    class main
    {
        
        static void Main(string[] args)
        {

            MicroscopeAnalyzer mAnalyzer = new MicroscopeAnalyzer();
        
            while(true)
            {
                Console.WriteLine("Enter in 'a' to take measurement, enter in 's' to save, 'l' to load");
                string input = Console.ReadLine();
                if (input == "a")
                {
                    Console.WriteLine("Now measuring!");
                    mAnalyzer.Measure();
                    if (mAnalyzer.mLastRet == 1) Console.WriteLine("Something went wrong in the last step!");
                }
                else if (input == "s")                                // Current file directory : C:\ProgramData\Filmetrics\Material
                {
                    mAnalyzer.mMeasuredResults.ret = "Saved!";

                    Console.WriteLine("Please enter in the name of the file you want it to be saved in.");
                    Console.WriteLine("The program will automatically save it as a '.fmspe' file. ");
                    Console.WriteLine("For now, all files will be saved in directory 'C:/Users/HMNL/Documents/Test/' ");
                    string currFileDir = "C:/Users/HMNL/Documents/Test/";
                    string user_input = Console.ReadLine();
                   
                    mAnalyzer.SaveSpectrum(currFileDir, user_input);
                    if (mAnalyzer.mLastRet == 1) Console.WriteLine("Something went wrong in the last step!");
                    mAnalyzer.SaveResultsTo(currFileDir, user_input);
                    if (mAnalyzer.mLastRet == 1) Console.WriteLine("Something went wrong in the last step!");
                }
                else if (input == "l")
                {
                    Console.WriteLine("Please enter in the name of the file you want the settings loaded to.");
                    Console.WriteLine("For now, all files will be saved in directory 'C:/Users/HMNL/Documents/Test/' ");
                    string currFileDir = "C:/Users/HMNL/Documents/Test/";
                    string user_input = Console.ReadLine();

                    Result loaded_result = MicroscopeAnalyzer.LoadMicroscopeAnalyzerFrom(currFileDir, user_input);
                    
                    if (loaded_result == null)
                    {
                        Console.WriteLine("Something wrong in the last step!");
                        continue;
                    }

                    Console.WriteLine("Load successful!");

                    Console.WriteLine("The wavelengths are");
                    for (int i = 0; i < loaded_result.PrimaryWavelengths.Length; ++i) Console.WriteLine(loaded_result.PrimaryWavelengths[i]);

                    Console.WriteLine("The summary is " + loaded_result.ResultsSummary);
                }
            }
        }

        
        // static void Main(string[] args)
        // {
        //     // XmlSerializer ser = new XmlSerializer(typeof(RandomTest));
        //     // 
        //     // RandomTest test;
        //     // 
        //     // // Read
        //     // using (var stream = File.OpenRead("C:/Users/Huafeng/Desktop/LoadingTest/test.xml"))
        //     // {
        //     //     test = (RandomTest)ser.Deserialize(stream);
        //     // }
        //     // 
        //     // Console.WriteLine(test.m_x);
        //     // Console.WriteLine(test.m_y);
        //     // Console.ReadLine();
        // 
        //     RandomTest a = new RandomTest(1, 2);
        //     
        //     DataContractSerializer ser = new XmlSerializer(typeof(RandomTest));
        //     
        //     // Write
        //     using (var stream = File.Create("C:/Users/Huafeng/Desktop/LoadingTest/test.xml"))
        //     {
        //         ser.Serialize(stream, a);
        //     }
        //     
        //     Console.WriteLine("Test saved!");
        // }
    }
}
