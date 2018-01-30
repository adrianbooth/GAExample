using System;
using System.Collections.Generic;
using System.Linq;

namespace GAExample.Helpers
{
    public static class Helpers
    {
        public static double StandardDeviation(IEnumerable<double> values)
        {
            double ret = 0;
            if (!values.Any())
            {
                return ret;
            }

            var avg = values.Average();
            var sum = values.Sum(d => Math.Pow(d - avg, 2));
            ret = Math.Sqrt((sum) / (values.Count() - 1));
            return ret;
        }
    }
}
