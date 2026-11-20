using System;
using System.Collections.Generic;
using System.Text;

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

        public static void PrintList(List<int> flowResult)
        {
            foreach (int value in flowResult)
            {
                Console.Write($"Values: ");
                Console.Write($"{value} ");
            }
            Console.WriteLine();
        }
    }

}
