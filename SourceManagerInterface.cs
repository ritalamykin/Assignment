using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnalyzeAPI
{
    public interface SourceManagerInterface
    {
        // Retuns a list of strings parsed from the json.
        public List<string> ParseJson(dynamic json);

        // Retunrns the URL to query.
        public string GetUrl();
    }
}
