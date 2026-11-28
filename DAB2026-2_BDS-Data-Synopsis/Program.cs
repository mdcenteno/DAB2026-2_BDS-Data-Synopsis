namespace DAB2026_2_BDS_Data_Synopsis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int flowSize = 100000;
            int maxValue = 20;

            FlowGenerator flowGenerator = new FlowGenerator(maxValue);
            ReservoirSampling reservoirSampling = new ReservoirSampling(100);
            const int snapshotCount = 6;
            int nextSnapshot = 1;

            for (int i = 0; i < flowSize; i++)
            {
                int item;
                //item = flowGenerator.spitOutRandom();
                //item = flowGenerator.spitOutBellCurve(3.0);
                //item = flowGenerator.spitOutBellCurveWithMovingPeak(1.0, flowSize);
                //item = flowGenerator.spitOutBellCurveWithTwoPeaks(1.0, flowSize);
                item = flowGenerator.spitOutBellCurveWithRandomPeaks(1.0, flowSize, 3);
                reservoirSampling.Receive(item);

                int processedItems = i + 1;
                int snapshotIteration = (flowSize * nextSnapshot + snapshotCount - 1) / snapshotCount;
                if (processedItems >= snapshotIteration)
                {
                    Console.WriteLine($"Processed Items: {processedItems}");
                    Console.WriteLine($"Flow Generator Snapshot {nextSnapshot}/{snapshotCount} ({processedItems * 100 / flowSize}%):");
                    Printer.PrintHistogramBar(flowGenerator.FlowResultHistogram);
                    nextSnapshot++;
                }
            }

            Console.WriteLine("Final results:");

            Console.WriteLine("Reservoir Sampling Results:");
            Printer.PrintHistogramBar(reservoirSampling.PublishResults());

            Console.WriteLine("Flow Generator Histogram:");
            Printer.PrintHistogramBar(flowGenerator.FlowResultHistogram);

            //Console.WriteLine("Flow Generator Result:");
            //Printer.PrintList(flowGenerator.FlowResult);
        }
    }
}
