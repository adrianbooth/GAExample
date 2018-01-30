using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GAExample;
using GAExample.Helpers;

public class WebPagePrinter : IPopulationPrinter
{
    private readonly StringBuilder FileOutput;

    public WebPagePrinter()
    {
        FileOutput = new StringBuilder();
        FileOutput.AppendLine(@"<!DOCTYPE html><html><head><style>table {    font-family: ""Trebuchet MS"", Arial, Helvetica, sans-serif;    border-collapse: collapse;    width: 100%;} td, th {    border: 1px solid #ddd;    padding: 8px;} tr:nth-child(even){background-color: #f2f2f2;} tr:hover {background-color: #ddd;} th {    padding-top: 12px;    padding-bottom: 12px;    text-align: left;    background-color: #4CAF50;    color: white;}</style></head><body><table>  <tr>    <th>Best Solution</th>    <th>Value</th>    <th>Standard Deviation</th>  </tr>");
    }

    public void OutputPopulationStatistics(List<Chromosome> population)
    {
        var bestSln = population.Max(e => e.Solution);
        var bestSlnValue = population.First(e => e.Solution == bestSln)?.Value;
        var standardDeviation = Helpers.StandardDeviation(population.Select(e => (double)e.Value));

        FileOutput.AppendLine($@" <tr>    <td>{bestSln}</td>    <td>{bestSlnValue}</td>    <td>{standardDeviation}</td>");
        Console.WriteLine($"Standard deviation: {standardDeviation} Best Solution: {bestSln} Individual value:{bestSlnValue}");
    }

    public void WriteResultsToFile()
    {
        FileOutput.AppendLine(@"</table></body></html>");
        File.WriteAllText("results.html", FileOutput.ToString());
    }
}