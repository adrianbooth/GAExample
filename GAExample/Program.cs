using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAExample.Helpers;

namespace GAExample
{
    class Program
    {
        // Values to tune algorithm
        private static readonly int PopulationSize = 30;
        private static readonly int Generations = 30;
        private static readonly double ChanceOfRandomMutation = 0.25;

        private static readonly double SecondBoundary = 1 - ChanceOfRandomMutation;
        private static readonly double FirstBoundary = (1 - ChanceOfRandomMutation) / 2;
        private static int _totalWeight;

        private static readonly Random Random;
        public static List<Chromosome> Population { get; set; }
        public static StringBuilder FileOutput { get; set; }
        private static IPopulationPrinter _printer;

        static Program()
        {
            Random = new Random((int)DateTime.UtcNow.Ticks);
            _printer = new ConsolePrinter();
            Population = new List<Chromosome>();

        }

        static void Main()
        {
            InitialisePopulation();

            for (var i = 0; i < Generations; i++)
            {
                EvaluateFitnessOfIndividuals();

                CreateOffspring();
            }

            _printer.WriteResultsToFile();
            Console.ReadLine();
        }

        private static void InitialisePopulation()
        {
            for (var i = 0; i < PopulationSize; i++)
            {
                Population.Add(new Chromosome(Random));
            }
        }

        private static void EvaluateFitnessOfIndividuals()
        {
            // check individuals ability to solve problem
            foreach (var chromosome in Population)
            {
                chromosome.Solution = (-1d * (chromosome.Value * ((double)chromosome.Value))) + (12d * chromosome.Value) - 7d;
            }

            // assign weights
            Population = Population.OrderByDescending(e => e.Solution).ToList();
            for (var index = 0; index < PopulationSize; index++)
            {
                var chromosome = Population[index];
                chromosome.Weight = (Population.Count - index + 2) / 1 + 2;

                // promote top 10%
                if (index < PopulationSize * 0.1)
                {
                    chromosome.Weight += (15 / (index + 1));
                }
            }

            _totalWeight = Population.Sum(e => e.Weight);

            _printer.OutputPopulationStatistics(Population);

        }

        private static void CreateOffspring()
        {
            var offspring = new List<Chromosome>();
            do
            {
                var firstParent = SelectParent();
                var secondParent = SelectParent();
                offspring.Add(ProduceOffspring(firstParent, secondParent));
            } while (offspring.Count < PopulationSize);

            Population = offspring;
        }

        private static Chromosome ProduceOffspring(Chromosome firstParent, Chromosome secondParent)
        {
            char[] newChromosome = new char[32];
            for (var i = 0; i < 32; i++)
            {
                var selection = Random.NextDouble();
                if (selection < FirstBoundary)
                {
                    newChromosome[i] = firstParent.Genes[i];
                }
                else if (selection >= FirstBoundary && selection < SecondBoundary)
                {
                    newChromosome[i] = secondParent.Genes[i];
                }
                else
                {
                    newChromosome[i] = Random.Next(0, 1).ToString().First();
                }
            }
            return new Chromosome(newChromosome);
        }

        private static Chromosome SelectParent()
        {
            var randomNumber = Random.Next(0, _totalWeight);

            Chromosome parent = null;
            foreach (var chromosome in Population)
            {
                if (randomNumber <= chromosome.Weight)
                {
                    parent = chromosome;
                    break;
                }

                randomNumber = randomNumber - chromosome.Weight;
            }

            return parent;
        }

    }
}