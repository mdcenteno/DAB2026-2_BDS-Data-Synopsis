using System;
using System.Collections.Generic;

namespace DAB2026_2_BDS_Data_Synopsis
{
    public static class Printer
    {
        public static void PrintHistogram(SortedDictionary<int, int> histogram)
        {
            foreach (var kvp in histogram)
            {
                Console.WriteLine($"Value: {kvp.Key}, Count: {kvp.Value}");
            }
            Console.WriteLine();
        }


        public static void PrintHistogramBar(SortedDictionary<int, int> histogram, int maxBarWidth = 50)
        {
            ArgumentNullException.ThrowIfNull(histogram);

            if (maxBarWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxBarWidth), "A largura máxima da barra deve ser maior que zero.");
            }

            if (histogram.Count == 0)
            {
                Console.WriteLine("Nenhum dado para exibir.");
                Console.WriteLine();
                return;
            }

            int maxCount = 0;
            foreach (int count in histogram.Values)
            {
                if (count > maxCount)
                {
                    maxCount = count;
                }
            }

            int valueWidth = 0;
            foreach (int value in histogram.Keys)
            {
                valueWidth = Math.Max(valueWidth, value.ToString().Length);
            }

            foreach (KeyValuePair<int, int> entry in histogram)
            {
                int barWidth = maxCount == 0
                    ? 0
                    : Math.Max(1, (int)Math.Round(entry.Value * (double)maxBarWidth / maxCount));

                Console.Write(entry.Key.ToString().PadLeft(valueWidth));
                Console.Write(" | ");
                Console.Write(new string('#', barWidth));
                Console.WriteLine($" {entry.Value}");
            }

            Console.WriteLine();
        }

        public static void PrintList(List<int> flowResult)
        {
            ArgumentNullException.ThrowIfNull(flowResult);

            Console.Write("Values: ");
            foreach (int value in flowResult)
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();
        }
    }

}
