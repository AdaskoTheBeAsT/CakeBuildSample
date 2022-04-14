using Cake.Frosting;

namespace Build.Tasks;

[TaskName("DefaultTest")]
[IsDependentOn(typeof(CleanReportsTask))]
[IsDependentOn(typeof(CreateReportsDirectoryTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(DotNetBuildQuickTask))]
[IsDependentOn(typeof(UnitTestTask))]

// [IsDependentOn(typeof(UnitTestConsolidateReportsTask))]
[IsDependentOn(typeof(UnitTestReportTask))]
[IsDependentOn(typeof(TestCoverageReportTask))]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class DefaultTestTask
    : FrostingTask<BuildContext>
{
}
