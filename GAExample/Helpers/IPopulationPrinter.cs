using System.Collections.Generic;

namespace GAExample.Helpers
{
    public interface IPopulationPrinter
    {
        void OutputPopulationStatistics(List<Chromosome> population);
        void WriteResultsToFile();
    }
}
