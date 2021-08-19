using System;
using System.Collections.Generic;

namespace AnalyzeAPI
{
    // AnalysisFlow
    public class AnalysisFlow
    {
        List<Func<string[], string[]>> filters;

        public AnalysisFlow(List<Func<string[], string[]>> filters)
        {
            this.filters = filters;
        }

        // Ready functions to be used in Analysis Flows //

        //Returns a list with the same elements as list, but without spaces
        public static string[] removeSpaces(string[] list)
        {
            List<String> res = new List<string>();
            foreach (string item in list)
            {
                string s = item.Replace(" ", "");
                res.Add(s);
            }
            return res.ToArray();
        }

        public List<Func<string[], string[]>> getFilters()
        {
            return this.filters;
        }

        // Returns a modified list that contains all items in the list containing 5 or more characters.
        public static string[] removeShortItems(string[] list)
        {
            List<string> tempList = new List<string>();
            foreach (string item in list)
            {
                if (item.Length >= 5)
                {
                    tempList.Add(item);
                }
            }
            return tempList.ToArray();
        }

        //Returns a list with the same elements as list, but all elements in lower case
        public static string[] toLowerCase(string[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = list[i].ToLower();
            }
            return list;
        }

        // Default flows //
        public static Dictionary<long, AnalysisFlow> defaultFlows = new Dictionary<long, AnalysisFlow>()
        {
            {1, new AnalysisFlow(new List<Func<string[], string[]>> { AnalysisFlow.removeSpaces }) },
            {2, new AnalysisFlow(new List<Func<string[], string[]>> { AnalysisFlow.removeShortItems })},
            {3, new AnalysisFlow(new List<Func<string[], string[]>> { AnalysisFlow.removeShortItems, AnalysisFlow.toLowerCase })},
            {4, new AnalysisFlow(new List<Func<string[], string[]>> { AnalysisFlow.removeShortItems, AnalysisFlow.toLowerCase, AnalysisFlow.removeSpaces })}
        };
    }
}
