using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filmetrics;

namespace FilmetricsAnalysis
{

    /** A modified version of the Result class provided by the FIRemote so it can be serialized and saved. **/

    public class Result
    {
        public string ret;


        public bool AlarmTriggered;
        public float GOF;
        public bool GofIsOK;
        public float[] LayerRoughnesses;
        public float[] LayerThicknesses;
        public float[] MeasFFTIntensity;
        public float[] MeasFFTThickness;
        public float[] PrimaryCalcSpectrum;
        public float[] PrimaryCalcWavelengths;
        public float[] PrimarySpectrum;
        public float[] PrimaryWavelengths;
        public string ResultsSummary;
        public System.Drawing.Image SampleImage;
        public float[] SpectrumAnalysisExtremaValues;
        public float[] SpectrumAnalysisExtremaWavelengthes;
        public float[] SpectrumAnalysisMeanValues;

        public Result() {}

        public Result(Filmetrics.FIRemote.FIMeasResults e)
        {
            this.AlarmTriggered = e.AlarmTriggered;
            this.GOF = e.GOF;
            this.GofIsOK = e.GofIsOK;
            this.LayerRoughnesses =                     (float[]) e.LayerRoughnesses.Clone();
            this.LayerThicknesses =                     (float[]) e.LayerThicknesses.Clone();
            this.MeasFFTIntensity =                     (float[]) e.MeasFFTIntensity.Clone();
            this.MeasFFTThickness =                     (float[]) e.MeasFFTThickness.Clone();
            this.PrimaryCalcSpectrum =                  (float[]) e.PrimaryCalcSpectrum.Clone();
            this.PrimaryCalcWavelengths =               (float[]) e.PrimaryCalcWavelengths.Clone();
            this.PrimarySpectrum =                      (float[]) e.PrimarySpectrum.Clone();
            this.PrimaryWavelengths =                   (float[]) e.PrimaryWavelengths.Clone();
            this.ResultsSummary =                       String.Copy(e.ResultsSummary);
            this.SampleImage =                          (System.Drawing.Image) e.SampleImage.Clone();
            this.SpectrumAnalysisExtremaValues =        (float[]) e.SpectrumAnalysisExtremaValues.Clone();
            this.SpectrumAnalysisExtremaWavelengthes =  (float[]) e.SpectrumAnalysisExtremaWavelengthes.Clone();
            this.SpectrumAnalysisMeanValues =           (float[]) e.SpectrumAnalysisMeanValues.Clone();
        }
    }
}
