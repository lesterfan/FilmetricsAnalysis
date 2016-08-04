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


        public bool AlarmTriggered { get; }
        public float GOF { get; }
        public bool GofIsOK { get; }
        public float[] LayerRoughnesses { get; }
        public float[] LayerThicknesses { get; }
        public float[] MeasFFTIntensity { get; }
        public float[] MeasFFTThickness { get; }
        public float[] PrimaryCalcSpectrum { get; }
        public float[] PrimaryCalcWavelengths { get; }
        public float[] PrimarySpectrum { get; }
        public float[] PrimaryWavelengths { get; }
        public string ResultsSummary { get; }
        public System.Drawing.Image SampleImage { get; }
        public float[] SpectrumAnalysisExtremaValues { get; }
        public float[] SpectrumAnalysisExtremaWavelengthes { get; }
        public float[] SpectrumAnalysisMeanValues { get; }

        public Result() {}

        public Result(Filmetrics.FIRemote.FIMeasResults e)
        {
            this.AlarmTriggered = e.AlarmTriggered;
            this.GOF = e.GOF;
            this.GofIsOK = e.GofIsOK;
            this.LayerRoughnesses = e.LayerRoughnesses;
            this.LayerThicknesses = e.LayerThicknesses;
            this.MeasFFTIntensity = e.MeasFFTIntensity;
            this.MeasFFTThickness = e.MeasFFTThickness;
            this.PrimaryCalcSpectrum = e.PrimaryCalcSpectrum;
            this.PrimaryCalcWavelengths = e.PrimaryCalcWavelengths;
            this.PrimarySpectrum = e.PrimarySpectrum;
            this.PrimaryWavelengths = e.PrimaryWavelengths;
            this.ResultsSummary = e.ResultsSummary;
            this.SampleImage = e.SampleImage;
            this.SpectrumAnalysisExtremaValues = e.SpectrumAnalysisExtremaValues;
            this.SpectrumAnalysisExtremaWavelengthes = e.SpectrumAnalysisExtremaWavelengthes;
            this.SpectrumAnalysisMeanValues = e.SpectrumAnalysisMeanValues;
        }
    }
}
