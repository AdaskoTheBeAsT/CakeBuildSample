using Build.Utils;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("UnitTestConsolidateReports")]
public class UnitTestConsolidateReportsTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var unitTestReportPath = new DirectoryPath(Constants.BackendUnitTestPath)
            .MakeAbsolute(context.Environment);
        var reportGeneratorSettings = new ReportGeneratorSettings
        {
            ReportTypes = new[] { ReportGeneratorReportType.Cobertura },
        };

        context.ReportGenerator2(
            new GlobPattern($"{unitTestReportPath.FullPath}/**/coverage.cobertura.xml"),
            unitTestReportPath,
            reportGeneratorSettings);
    }
}
