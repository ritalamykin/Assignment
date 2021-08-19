using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AnalyzeAPI
{
    // TODO - Check comments.
    // AnalyzeAPI has two functions:
    // 1. Analyze(string) which will return an array of strings according to the data source string input.
    // 2. Analyze(string, long) which will call Analyze(string) and then perform filters according to predefined flows.
    public class AnalyzeAPI : AnalyzeAPIInterface
    {

        // Static members - HTTP client and handler //

        private static readonly HttpClientHandler handler = new HttpClientHandler();
        private static readonly HttpClient client = new HttpClient(handler);

        static AnalyzeAPI()
        {
            setUp();
        }

        public static void setUp()
        {
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Rita's Assignment for NI");
        }

        // Constructor and per instance fields //

        private Dictionary<long, AnalysisFlow> flows;

        public AnalyzeAPI(Dictionary<long, AnalysisFlow> flows)
        {
            this.flows = flows;
        }

        public AnalyzeAPI() {
            this.flows = AnalysisFlow.defaultFlows;
            Console.WriteLine("AnalyzeAPI");
        }

        // Overriden methods //

        public string[] Analyze(string dataSourceName)
        {
            SourceManagerInterface manager;
            switch (dataSourceName)
            {
                case "Stackoverflow":
                    manager = new StackOverflowSourceManager();
                    break;
                case "Github":
                    manager = new GitHubSourceManager();
                    break;
                default:
                    return new string[]{};
            }
            string json = getResponseString(manager.GetUrl());
            var jsonData = JsonConvert.DeserializeObject<dynamic>(json);
            List<string> retList = manager.ParseJson(jsonData);
            return retList.ToArray();
        }

        public string[] Analyze(string dataSourceName, long id)
        {
            string[] res = Analyze(dataSourceName);
            // run all the functions under flows[id];
            List<Func<string[], string[]>> filters = flows[id].getFilters();
            foreach (var func in filters)
            {
                res = func(res);
            }
            return res;
        }

        // Returns a json string that is returned from calling the API in the
        // sourceURLMap[dataSourceName].
        private static string getResponseString(string Url)
        {
            var responseString = client.GetAsync(Url).Result;
            string res = responseString.Content.ReadAsStringAsync().Result;
            return res;
        }

        public static void Main(string[] args)
        {
            // For debugging.
        }
    }

}

