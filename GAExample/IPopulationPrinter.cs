using System.Collections.Generic;

namespace GAExample
{
    public interface IPopulationPrinter
    {
        void OutputPopulationStatistics(List<Chromosome> population);
        void WriteResultsToFile();
    }
}
