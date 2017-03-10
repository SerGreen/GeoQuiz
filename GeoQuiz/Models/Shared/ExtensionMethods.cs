using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models.Shared
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
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

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rnd)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = rnd.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
    }
}