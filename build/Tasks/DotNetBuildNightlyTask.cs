using Cake.Frosting;

namespace Build.Tasks;

[TaskName("DotNetBuildNightly")]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(CreateFoldersTask))]
[IsDependentOn(typeof(YarnInstallTask))]

// [IsDependentOn(typeof(YarnRunBuildTask))]
// [IsDependentOn(typeof(YarnRunTestAllTask))]
// [IsDependentOn(typeof(YarnRunHtmlHintReportTask))]
// [IsDependentOn(typeof(YarnRunStyleLintReportTask))]
// [IsDependentOn(typeof(YarnRunEsLintReportTask))]
// [IsDependentOn(typeof(YarnRunSonarTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(SonarBeginTask))]
[IsDependentOn(typeof(DotNetBuildTask))]
[IsDependentOn(typeof(UnitTestTask))]
[IsDependentOn(typeof(IntegrationTestTask))]
[IsDependentOn(typeof(SonarEndTask))]

// [IsDependentOn(typeof(UnitTestConsolidateReportsTask))]
[IsDependentOn(typeof(UnitTestReportTask))]

// [IsDependentOn(typeof(IntegrationTestConsolidateReportsTask))]
[IsDependentOn(typeof(IntegrationTestReportTask))]
[IsDependentOn(typeof(TestCoverageReportTask))]
[IsDependentOn(typeof(AdjustFrontendCoverageReportTask))]
[IsDependentOn(typeof(CombineCoverageReportTask))]
[IsDependentOn(typeof(CombineCoverageHtmlReportTask))]
[IsDependentOn(typeof(DotNetBuildDbTask))]
[IsDependentOn(typeof(ReSharperInspectCodeTask))]
[IsDependentOn(typeof(ReSharperInspectCodeReportTask))]
[IsDependentOn(typeof(BuildReportTask))]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class DotNetBuildNightlyTask
    : FrostingTask<BuildContext>
{
}
