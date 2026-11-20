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

        public int spitOutBellCurve()
        {
            index++;

            // Somando 4 números aleatórios para formar a Bell Curve centrada no meio do intervalo
            int sum = 0;
            const int rolls = 4;
            for (int i = 0; i < rolls; i++)
            {
                sum += random.Next(0, maxValue);
            }

            // Média dos lançamentos mantém o valor final dentro de [0, maxValue)
            int value = sum / rolls;

            Register(value);

            return value;
        }
    }
}
