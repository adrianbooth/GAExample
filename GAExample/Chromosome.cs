using System;
using System.Diagnostics;
using System.Linq;

namespace GAExample
{
    /// <summary>
    /// A possible solution to the problem, normally referred to as a Chromosome or Individual
    /// </summary>
    [DebuggerDisplay("X = {Solution}")]  
    public class Chromosome : IComparable<Chromosome>
    {
        private int _weight;
        public Chromosome() { }

        public Chromosome(char[] genes)
        {
            Value = Convert.ToInt32(new string(genes), 2);
        }
        public Chromosome(Random random)
        {
            Value = random.Next(-2147483647, 2147483647);
        }

        private char[] _genes;
        public char[] Genes
        {
            get
            {
                if (_genes == null)
                    _genes = Convert.ToString(Value, 2).PadLeft(32, '0').ToArray();

                return _genes;
            }
        }

        public int Value { get; set; }
        public double Solution { get; set; }

        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public int CompareTo(Chromosome other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var valueComparison = Weight.CompareTo(other.Weight);
            if (valueComparison != 0) return valueComparison;
            return Weight.CompareTo(other.Weight);
        }
    }
}