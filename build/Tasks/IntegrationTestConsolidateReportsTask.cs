using Build.Utils;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("IntegrationTestConsolidateReports")]
public class IntegrationTestConsolidateReportsTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var integrationTestReportPath = new DirectoryPath(Constants.BackendIntegrationTestPath)
            .MakeAbsolute(context.Environment);
        var reportGeneratorSettings = new ReportGeneratorSettings
        {
            ReportTypes = new[] { ReportGeneratorReportType.Cobertura },
        };

        context.ReportGenerator2(
            new GlobPattern($"{integrationTestReportPath.FullPath}/**/coverage.cobertura.xml"),
            integrationTestReportPath,
            reportGeneratorSettings);
    }
}
