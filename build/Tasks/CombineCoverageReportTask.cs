using Build.Utils;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CombineCoverageReport")]
public class CombineCoverageReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var backendUnitReportPath =
            new DirectoryPath(Constants.BackendUnitTestPath)
                .MakeAbsolute(context.Environment);
        var backendUnitCoverageFilePath = backendUnitReportPath
            .CombineWithFilePath(Constants.CoberturaFileName);
        var backendIntegrationReportPath =
            new DirectoryPath(Constants.BackendIntegrationTestPath)
                .MakeAbsolute(context.Environment);
        var backendIntegrationCoverageFilePath = backendIntegrationReportPath
            .CombineWithFilePath(Constants.CoberturaFileName);
        var frontendUnitReportPath =
            new DirectoryPath(Constants.FrontendUnitTestPath)
                .MakeAbsolute(context.Environment);
        var frontendCoverageFilePath = frontendUnitReportPath
            .Combine(Constants.FrontendUiPath)
            .Combine(Constants.CoverageDirectoryName)
            .CombineWithFilePath(Constants.CoverageAdjustedCoberturaFileName);
        var outputPath = new DirectoryPath(Constants.CombinedCoverageReportPath)
            .MakeAbsolute(context.Environment);

        var reportGeneratorSettings = new ReportGeneratorSettings
        {
            ReportTypes = new[] { ReportGeneratorReportType.Cobertura },
        };

        context.ReportGenerator(
            new[]
            {
                backendUnitCoverageFilePath,
                backendIntegrationCoverageFilePath,
                frontendCoverageFilePath,
            },
            outputPath,
            reportGeneratorSettings);
    }
}
