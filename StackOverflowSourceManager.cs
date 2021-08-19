using System;
using System.Collections.Generic;

namespace AnalyzeAPI
{
    public class StackOverflowSourceManager : SourceManagerInterface
    {
        public List<string> ParseJson(dynamic json)
        {
            List<string> retList = new List<string>();
            foreach (var item in json["items"])
            {
                string s = item["title"];
                retList.Add(s);
            }
            return retList;
        }

        public string GetUrl()
        {
            return "https://api.stackexchange.com/2.2/tags/highcharts/faq?site=stackoverflow";
        }
    }
}