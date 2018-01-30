using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GAExample
{
    public class ConsolePrinter : IPopulationPrinter
    {
        private readonly StringBuilder FileOutput;

        public ConsolePrinter()
        {
            FileOutput = new StringBuilder();
            FileOutput.AppendLine(@"""Best Solution"",""Value"",""Standard Deviation""");
        }

        public void OutputPopulationStatistics(List<Chromosome> population)
        {
            var bestSln = population.Max(e => e.Solution);
            var bestSlnValue = population.First(e => e.Solution == bestSln)?.Value;
            var standardDeviation = Helpers.StandardDeviation(population.Select(e => (double)e.Value));

            FileOutput.AppendLine($@"""{bestSln}"",""{bestSlnValue}"",""{standardDeviation}""");
            Console.WriteLine($"Standard deviation: {standardDeviation} Best Solution: {bestSln} Individual value:{bestSlnValue}");
        }

        public void WriteResultsToFile()
        {
            File.WriteAllText("results.csv", FileOutput.ToString());
        }
    }
}