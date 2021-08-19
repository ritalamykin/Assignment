using System;
using System.Collections.Generic;

namespace AnalyzeAPI
{
    public class GitHubSourceManager : SourceManagerInterface
    {
        public List<string> ParseJson(dynamic json)
        {
            List<string> retList = new List<string>();
            foreach (var item in json)
            {
                string s = item["commit"]["message"];
                retList.Add(s);
            }
            return retList;
        }

        public string GetUrl()
        {
            return "https://api.github.com/repos/highcharts/highcharts/commits";
        }
    }
}
