using System;
using System.Collections.Generic;
using Sara.LogReader.Model.PatternNS;

namespace Sara.LogReader.Model.IDE
{
    public interface IPatternTestResult
    {
        int EndiLine { get; set; }
        List<PatternTestResultEventPattern> Events { get; set; }
        string Name { get; set; }
        PatternOptions Options { get; set; }
        string Path { get; set; }
        string PatternName { get; set; }
        int StartiLine { get; set; }
        TimeSpan TotalDuration { get; set; }
        List<string> Warnings { get; set; }
    }
}