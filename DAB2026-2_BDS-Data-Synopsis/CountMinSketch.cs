using System;
using System.Collections.Generic;

namespace DAB2026_2_BDS_Data_Synopsis
{
    public class CountMinSketch
    {
        private readonly int[,] table;
        private readonly uint[] seeds;
        private readonly HashSet<int> observedItems;

        public CountMinSketch(int depth, int width)
        {
            if (depth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depth), "A quantidade de linhas deve ser maior que zero.");
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "A largura deve ser maior que zero.");
            }

            table = new int[depth, width];
            seeds = new uint[depth];
            observedItems = new HashSet<int>();

            for (int row = 0; row < depth; row++)
            {
                seeds[row] = unchecked(0x9e3779b9u * (uint)(row + 1));
            }
        }

        public int Depth => table.GetLength(0);
        public int Width => table.GetLength(1);

        public int Receive(int item)
        {
            observedItems.Add(item);

            for (int row = 0; row < Depth; row++)
            {
                int column = Hash(item, row);
                table[row, column]++;
            }

            return Estimate(item);
        }

        public int Estimate(int item)
        {
            int estimate = int.MaxValue;

            for (int row = 0; row < Depth; row++)
            {
                int column = Hash(item, row);
                estimate = Math.Min(estimate, table[row, column]);
            }

            return estimate;
        }

        public SortedDictionary<int, int> PublishResults()
        {
            SortedDictionary<int, int> output = new SortedDictionary<int, int>();

            foreach (int item in observedItems)
            {
                output[item] = Estimate(item);
            }

            return output;
        }

        private int Hash(int item, int row)
        {
            uint hash = unchecked((uint)item);
            hash ^= seeds[row];
            hash ^= hash >> 16;
            hash *= 0x7feb352du;
            hash ^= hash >> 15;
            hash *= 0x846ca68bu;
            hash ^= hash >> 16;

            return (int)(hash % (uint)Width);
        }
    }
}
