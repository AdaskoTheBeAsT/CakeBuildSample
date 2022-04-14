using Cake.Frosting;

namespace Build.Tasks;

[TaskName("MsBuildNightly")]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(CreateFoldersTask))]
[IsDependentOn(typeof(NuGetRestoreTask))]
[IsDependentOn(typeof(SonarBeginTask))]
[IsDependentOn(typeof(MsBuildTask))]
[IsDependentOn(typeof(UnitTestTask))]
[IsDependentOn(typeof(IntegrationTestTask))]
[IsDependentOn(typeof(SonarEndTask))]
[IsDependentOn(typeof(UnitTestReportTask))]
[IsDependentOn(typeof(IntegrationTestReportTask))]
[IsDependentOn(typeof(TestCoverageReportTask))]
[IsDependentOn(typeof(ReSharperInspectCodeTask))]
[IsDependentOn(typeof(ReSharperInspectCodeReportTask))]
[IsDependentOn(typeof(BuildReportTask))]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class MsBuildNightlyTask
    : FrostingTask<BuildContext>
{
}
