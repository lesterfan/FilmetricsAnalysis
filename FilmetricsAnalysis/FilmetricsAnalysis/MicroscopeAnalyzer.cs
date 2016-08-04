﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Filmetrics;

namespace FilmetricsAnalysis
{
    public class MicroscopeAnalyzer
    {
        public FIRemote mFIRemote;
        public string mReferenceMaterial;
        public Filmetrics.FIRemote.FIMeasResults mMeasuredResults;

        // Test : If loaded from save, it will be a different value
        public string mTestString;

        // If mLastRet = 1, it means that something has gone wrong in the previous step
        public int mLastRet = 0;

        // Constructor
        public MicroscopeAnalyzer()
        {
            Console.WriteLine("Welcome to Filmetrics Analyzer - HMNL (c) 2016");
            try
            {
                mFIRemote = new FIRemote(true);
                Console.WriteLine("Software was successfully connected to the microscope!");
                mLastRet = 0;
            }
            catch (FIRemote.InitializationFailureException e)
            {
                Console.WriteLine("Error : Unable to access the microscope hardware!");
                mLastRet = 1;
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception! : " + e.ToString());
                mLastRet = 1;
                return;
            }
        }

        // ----------------------------------- Methods ---------------------------------------------

        /*  Baseline Step 0 : Obtains the reference material from the user    */
        public void obtainRefMat()
        {
            Console.WriteLine("Please type in which material you are referencing.");
            mReferenceMaterial = Console.ReadLine();
            mFIRemote.BaselineSetRefMat(mReferenceMaterial);
        }

        /*  Baseline Step 1 : Acquire sample reflectance     */
        public void BaselineStep1()
        {
            try
            {
                mFIRemote.BaselineAcquireSpectrumFromSample();
                Console.WriteLine("Sample reflectance successful!");
                mLastRet = 0;
            }
            catch (Filmetrics.FIRemote.AcquisitionException e)
            {
                if (e.Message == "") Console.WriteLine("Spectrum acquisition errror.");
                else Console.WriteLine("Error attempting to acquire spectrum. Exception message is: " + e.Message);
                mLastRet = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown Exception caught!" + e.ToString());
                mLastRet = 1;
            }
        }

        /*  Baseline Step 2 : Acquire reference standard   */
        public void BaselineStep2()
        {
            Console.WriteLine("I am now acquiring reference standard");
            try
            {
                mFIRemote.BaselineAcquireReference();
                Console.WriteLine("Reference standard successful!");
                mLastRet = 0;
            }
            catch (Filmetrics.FIRemote.AcquisitionException e)
            {
                if (e.Message == "")
                {
                    Console.WriteLine("Spectrum acquisition error!");
                }
                else
                {
                    Console.WriteLine("Error attempting to acquire spectrum.Exception message is: " + e.Message);
                }
                mLastRet = 1;
            }
            catch (Filmetrics.FIRemote.ArgumentException e)
            {
                Console.WriteLine("Bad argument. Exception caught. Message is: " + e.Message + "\nOffending argument is : " + e.ParamName + "\nNote: if acquisition mode is transmittance only, then the reference material should usually be set to 1 in order to avoid this error.");
                mLastRet = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! " + e.ToString());
                mLastRet = 1;
            }
        }

        //while (munchkinsInBox){
            //Lester.eatMunchkin()}

        /*  Baseline Step 3 : Take background   */
        public void BaselineStep3()
        {
            Console.WriteLine("Now taking a background.");
            try
            {
                mFIRemote.BaselineAcquireBackgroundAfterRef();
                Console.WriteLine("Acquire background successful!");
                mLastRet = 0;
            }
            catch (Filmetrics.FIRemote.AcquisitionException e)
            {
                if (e.Message == "")
                {
                    Console.WriteLine("Spectrum acquisition error.");
                }
                else
                {
                    Console.WriteLine("Error attempting to acquire spectrum.Exception message is: " + e.Message);
                }
                mLastRet = 1;
            }
            catch (Filmetrics.FIRemote.InvalidBackgroundException e)
            {
                Console.WriteLine("ERROR! : Background spectrum and reference spectrum are almost exactly the same. Please make sure that the reference sample has "
                    + "\nbeen removed from the stage and repeat acquisition of the Background spectrum. If this error happens again, you must cancel "
                    + "\nand start the baseline procedure over from the beginning.");

                mLastRet = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! " + e.ToString());
                mLastRet = 1;
            }
        }

        /*  Baseline Step 4 : Commit the baseline   */
        public void BaselineStep4()
        {
            try
            {
                mFIRemote.BaselineCommit();
                Console.WriteLine("Baseline successfully committed!");
                mLastRet = 0;
            }
            catch (Filmetrics.FIRemote.TimeOutException e)
            {
                Console.WriteLine("Error!" + e.Message);
                mLastRet = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! " + e.ToString());
                mLastRet = 1;
            }
        }

        /* 
        **  Acquires a baseline, does all 0-4 steps sequentially.
        **  Returns 1 if an error occured, 0 if baseline is successfully taken.
        **  */
        public void AcquireBaseline()
        {
            Console.WriteLine("Enter in anything to begin the baseline procedure.");
            Console.ReadLine();

            // Step 0
            obtainRefMat();

            // Step 1
            Console.WriteLine("Please focus the microscope and prepare to takeoff to step 1 (or to take a sample reflectance)!");
            Console.ReadLine();
            BaselineStep1();
            if (mLastRet == 1)
            {
                Console.WriteLine("Error! Something went wrong in the last step!");
                return;
            }

            // Step 2
            Console.WriteLine("Enter in anything to continue the journey to step 2 (aka to take a reflectance standard)!");
            Console.ReadLine();
            BaselineStep2();
            if (mLastRet == 1)
            {
                Console.WriteLine("Error! Something went wrong in the last step!");
                return;
            }

            // Step 3
            Console.WriteLine("Enter in anything to continue the journey to step 3 (aka to acquire background)!");
            Console.ReadLine();
            BaselineStep3();
            if (mLastRet == 1)
            {
                Console.WriteLine("Error! Something went wrong in the last step!");
                return;
            }

            // Step 4
            Console.WriteLine("Enter in anything to continue the journey to step 4 (aka to commit baseline)!");
            Console.ReadLine();
            BaselineStep4();
            if (mLastRet == 1)
            {
                Console.WriteLine("Error! Something went wrong in the last step!");
                return;
            }
        }


        /* 
        **  Emulates the function of clicking the measure button
        **  */
        public void Measure()
        {
            try
            {
                mMeasuredResults = mFIRemote.Measure(true);
                mLastRet = 0;
            }
            catch (Filmetrics.FIRemote.AcquisitionException e)
            {
                if (e.Type == FIRemote.AcquisitionException.ExceptionType.Saturation)
                {
                    Console.WriteLine("Spectrometer saturation. Repeat baseline or reduce integration time if acquisition settings measurement timing is set to manual.");
                }
                else if (e.Type == FIRemote.AcquisitionException.ExceptionType.InvalidAcquisitionSettings)
                {
                    Console.WriteLine("Invalid acquisition settings. Verify that a valid baseline has been established.");
                }
                else if (e.Type == FIRemote.AcquisitionException.ExceptionType.Unknown)
                {
                    if (e.Message == "")
                    {
                        Console.WriteLine("Unknown acquisition error. ");
                    }
                    else
                    {
                        Console.WriteLine("Error attempting to measure. Exception message is: " + e.Message);
                    }
                }
                mLastRet = 1;
            }
            catch (Filmetrics.FIRemote.SpectrumAnalysisException e)
            {
                Console.WriteLine("Error attempting to analyze spectrum. Error message is: " + e.Message);
                mLastRet = 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! " + e.ToString());
                mLastRet = 1;
            }
        }

        /* 
        **  Emulates the function of clicking the save button
        **  @param fileDir : directory that it should be saved in
        **  @param userInput : the name that the file should be stored as.
        **  */
        public void SaveSpectrum(string fileDir, string userInput)
        {
            try
            {
                Console.WriteLine("Currently saving spectrum to "+fileDir+userInput+".fmspe");

                // Save the spectrum file
                mFIRemote.SaveSpectrum(fileDir+userInput+".fmspe");

                Console.WriteLine("File saved!");
                mLastRet = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! " + e.ToString());
                mLastRet = 1;
            }
        }

        /* 
        **  Saves the current state of MicroscopeAnalyzer variable to an .xml file 
        **  @param fileDir : directory that it should be saved in
        **  @param userInput : the name that the file should be stored as
        **  */
        public void SaveMyselfTo(string fileDir, string userInput)
        {
            Console.WriteLine("Now saving myself to " + fileDir + userInput + ".xml");

            try {
                XmlSerializer ser = new XmlSerializer(typeof(MicroscopeAnalyzer));

                // Deserialize the variable from the specific directory indicated by user.
                using (var stream = File.Create(fileDir + userInput + ".xml"))
                {
                    ser.Serialize(stream, this);
                }

                Console.WriteLine("I saved myself!");
                mLastRet = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! " + e.ToString());
                mLastRet = 1;
            }
        }

        /* 
        **  Reverts the state of the variable 'this' to the state of MicroscopeAnalyzer saved in the .xml file specified by the user
        **  @param fileDir : directory that it should be saved in
        **  @param userInput : the name that the file should be stored as
        **  */
        public static MicroscopeAnalyzer LoadMicroscopeAnalyzerFrom(string fileDir, string userInput)
        {
            Console.WriteLine("Now loading myself to " + fileDir + userInput + ".xml");

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(MicroscopeAnalyzer));

                MicroscopeAnalyzer result;

                using (var stream = File.OpenRead(fileDir + userInput + ".xml"))
                {
                    result = (MicroscopeAnalyzer)ser.Deserialize(stream);
                }
                Console.WriteLine("Successfully loaded!");

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! "+e.ToString());
                return null;
            }
        }



    }
}
