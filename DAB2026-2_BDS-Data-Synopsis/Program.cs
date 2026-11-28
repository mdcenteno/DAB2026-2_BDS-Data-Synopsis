namespace DAB2026_2_BDS_Data_Synopsis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int flowSize = 100000;

            FlowGenerator flowGenerator = new FlowGenerator(20);
            ReservoirSampling reservoirSampling = new ReservoirSampling(100);

            for (int i = 0; i < flowSize; i++)
            {
                //int item = flowGenerator.spitOutRandom();
                int item = flowGenerator.spitOutBellCurve(3.0);
                reservoirSampling.Receive(item);
            }

            Console.WriteLine("Reservoir Sampling Results:");
            Printer.PrintHistogramBar(reservoirSampling.PublishResults());

            Console.WriteLine("Flow Generator Histogram:");
            Printer.PrintHistogramBar(flowGenerator.FlowResultHistogram);

            //Console.WriteLine("Flow Generator Result:");
            //Printer.PrintList(flowGenerator.FlowResult);
        }
    }
}
