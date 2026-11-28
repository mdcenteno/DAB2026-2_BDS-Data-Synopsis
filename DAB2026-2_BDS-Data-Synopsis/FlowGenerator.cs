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

        private void Register(int value)
        {
            FlowResult.Add(value);
            FlowResultHistogram[value] = FlowResultHistogram.TryGetValue(value, out int count)
                ? count + 1
                : 1;
        }

        public int spitOutBellCurve(double standardDeviation)
        {
            if (!double.IsFinite(standardDeviation) || standardDeviation < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(standardDeviation), "O desvio padrão deve ser finito e não negativo.");
            }

            index++;

            double mean = (maxValue - 1) / 2.0;
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
    }
}
