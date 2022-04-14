using Build.Utils;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Sonar;

namespace Build.Tasks;

[TaskName("TestCoverageReport")]
public class TestCoverageReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var unitReportPath = new DirectoryPath(Constants.BackendUnitTestPath).MakeAbsolute(context.Environment);

        GenerateReport(context, unitReportPath, ReportGeneratorReportType.Badges);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.Clover);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.Cobertura);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.CsvSummary);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.Html);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.HtmlChart);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.HtmlInline);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.HtmlInline_AzurePipelines);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.HtmlInline_AzurePipelines_Dark);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.HtmlSummary);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.JsonSummary);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.Latex);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.LatexSummary);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.lcov);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.MHtml);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.PngChart);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.SonarQube);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.TeamCitySummary);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.TextSummary);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.Xml);
        GenerateReport(context, unitReportPath, ReportGeneratorReportType.XmlSummary);
    }

    private void GenerateReport(
        BuildContext context,
        DirectoryPath unitReportPath,
        ReportGeneratorReportType reportType)
    {
        var outputPath = unitReportPath.Combine(new DirectoryPath(reportType.ToString("G")));
        var reportGeneratorSettings = new ReportGeneratorSettings
        {
           ReportTypes = new[] { reportType },
        };

        context.ReportGenerator(
            new GlobPattern($"{unitReportPath.FullPath}/**/{Constants.CoverageCoberturaFileName}"),
            outputPath,
            reportGeneratorSettings);
    }
}
