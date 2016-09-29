using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Infrastructure
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Shuffle<T>(this IList<T> list)
        {
            IList<T> copy = new List<T>(list);
            Random rnd = new Random();
            for (int i = copy.Count() - 1; i >= 0; i--)
            {
                int index = rnd.Next(0, copy.Count());
                yield return copy.ElementAt(index);
                copy.RemoveAt(index);
            }
        }
    }
}