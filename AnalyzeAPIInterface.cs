using System;

namespace AnalyzeAPI
{
    public interface AnalyzeAPIInterface
    {
        public string[] Analyze(string dataSourceName);

        public string[] Analyze(string dataSourceName, long id);
    }
}