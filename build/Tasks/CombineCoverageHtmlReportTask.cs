using Build.Utils;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CombineCoverageHtmlReport")]
public class CombineCoverageHtmlReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var combinedReportPath =
            new DirectoryPath(Constants.CombinedCoverageReportPath)
                .MakeAbsolute(context.Environment)
                .CombineWithFilePath(Constants.CoberturaFileName);
        var outputPath =
            new DirectoryPath(Constants.CombinedCoverageReportPath)
                .MakeAbsolute(context.Environment)
                .Combine(Constants.CoverageDirectoryName);

        var reportGeneratorSettings = new ReportGeneratorSettings
        {
            ReportTypes = new[] { ReportGeneratorReportType.HtmlInline_AzurePipelines },
        };

        context.ReportGenerator(
            combinedReportPath,
            outputPath,
            reportGeneratorSettings);
    }
}
