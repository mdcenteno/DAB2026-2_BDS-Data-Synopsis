using System;
using System.Collections.Generic;
using System.Text;

namespace DAB2026_2_BDS_Data_Synopsis
{
    public class FlowGenerator
    {
        public List<int> FlowResult { get; set; }
        public SortedDictionary<int, int> FlowResultHistogram { get; set; }

        Random random;
        int index;
        int maxValue;
        public FlowGenerator(int maxValue)
        {
            random ??= Random.Shared;
            index = 0;
            this.maxValue = maxValue;
            FlowResult = new List<int>();
            FlowResultHistogram = new SortedDictionary<int, int>();
        }

        public int spitOutRandom()
        {
            index++;
            int value = random.Next(0, maxValue);
            Register(value);
            return value;
        }

        public int spitOutBellCurve(double standardDeviation)
        {
            ValidateStandardDeviation(standardDeviation);
            index++;

            double mean = (maxValue - 1) / 2.0;
            return GenerateBellCurveValue(mean, standardDeviation);
        }

        public int spitOutBellCurveWithMovingPeak(double standardDeviation, int flowSize)
        {
            ValidateFlowSize(flowSize);
            ValidateStandardDeviation(standardDeviation);

            index++;
            double progress = flowSize == 1
                ? 0
                : Math.Clamp((index - 1.0) / (flowSize - 1), 0, 1);
            double mean = progress * (maxValue - 1);
            return GenerateBellCurveValue(mean, standardDeviation);
        }

        public int spitOutBellCurveWithTwoPeaks(double standardDeviation, int flowSize)
        {
            ValidateFlowSize(flowSize);
            ValidateStandardDeviation(standardDeviation);

            index++;
            int firstHalfSize = flowSize / 2 + flowSize % 2;
            double mean = index <= firstHalfSize
                ? maxValue * 0.25
                : maxValue * 0.75;

            return GenerateBellCurveValue(mean, standardDeviation);
        }

        private int GenerateBellCurveValue(double mean, double standardDeviation)
        {
            double valueAsDouble = mean;
            if (standardDeviation > 0)
            {
                double u1 = 1.0 - random.NextDouble();
                double u2 = random.NextDouble();
                double standardNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
                valueAsDouble = mean + standardDeviation * standardNormal;
            }

            int value = (int)Math.Round(Math.Clamp(valueAsDouble, 0, maxValue - 1));
            Register(value);
            return value;
        }

        private void Register(int value)
        {
            FlowResult.Add(value);
            FlowResultHistogram[value] = FlowResultHistogram.TryGetValue(value, out int count)
                ? count + 1
                : 1;
        }

        private static void ValidateStandardDeviation(double standardDeviation)
        {
            if (!double.IsFinite(standardDeviation) || standardDeviation < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(standardDeviation), "O desvio padrão deve ser finito e não negativo.");
            }
        }

        private static void ValidateFlowSize(int flowSize)
        {
            if (flowSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(flowSize), "O tamanho do fluxo deve ser maior que zero.");
            }
        }
    }
}
