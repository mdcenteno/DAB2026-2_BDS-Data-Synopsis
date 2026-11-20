using System;
using System.Collections.Generic;

namespace DAB2026_2_BDS_Data_Synopsis
{
    public class ReservoirSampling
    {
        /// <summary>
        /// Seleciona uma amostra aleatória uniforme de tamanho 'k' a partir de um fluxo (IEnumerable).
        /// Algoritmo R (Alan G. Waterman).
        /// </summary>
        /// <typeparam name="T">Tipo dos elementos do fluxo.</typeparam>
        /// <param name="stream">Fluxo de dados (finito ou infinito).</param>
        /// <param name="k">Tamanho do reservatório (tamanho da amostra desejada).</param>
        /// <param name="random">Instância opcional de Random para reprodutibilidade.</param>
        /// 
        List<int> reservoir;
        Random random;
        int k;
        int index;


        public ReservoirSampling(int k)
        {
            this.k = k;
            reservoir = new List<int>(k);
            random ??= Random.Shared;
        }

        public int Receive(int item)
        {
            index++;

            if (index <= k)
            {
                // Preenche o reservatório inicialmente com os primeiros k elementos
                reservoir.Add(item);
            }
            else
            {
                // Sorteia um inteiro no intervalo [0, index - 1]
                int j = random.Next(index);

                // Se o número sorteado for menor que k, substitui o item na posição j
                if (j < k)
                {
                    reservoir[j] = item;
                }
            }

            return index;
        }

        public SortedDictionary<int, int> PublishResults()
        {
            SortedDictionary<int, int> output = new SortedDictionary<int, int>();

            foreach (int sample in reservoir)
            {
                output[sample] = output.TryGetValue(sample, out int count)
                    ? count + 1
                    : 1;
            }

            return output;
        }

    }
}