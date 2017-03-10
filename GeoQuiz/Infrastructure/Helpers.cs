using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Infrastructure
{
    public static class Helpers
    {
        private static readonly string[] continents = { "NA", "SA", "EU", "AS", "AF", "AU" };

        public static string GetContinentsList(List<string> shorts)
        {
            List<string> list = new List<string>();
            bool[] isPresent = new bool[continents.Length];
            for (int i = 0; i < continents.Length; i++)
                if (shorts.Contains(continents[i]))
                    isPresent[i] = true;

            if (isPresent.All(x => x == true))
                return "Whole world";

            if (isPresent[0] && isPresent[1])
                list.Add("North and South America");
            else
            {
                if(isPresent[0])
                    list.Add("North America");
                if (isPresent[1])
                    list.Add("South America");
            }

            if (isPresent[2])
                list.Add("Europe");
            if (isPresent[3])
                list.Add("Asia");
            if (isPresent[4])
                list.Add("Africa");
            if (isPresent[5])
                list.Add("Australia and Oceania");

            return string.Join(" | ", list);
        }
    }
}