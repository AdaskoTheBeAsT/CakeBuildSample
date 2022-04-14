using Cake.Frosting;

namespace Build.Tasks;

[TaskName("MsBuildDaily")]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(CreateFoldersTask))]
[IsDependentOn(typeof(NuGetRestoreTask))]
[IsDependentOn(typeof(MsBuildQuickTask))]
[IsDependentOn(typeof(UnitTestQuickTask))]
[IsDependentOn(typeof(IntegrationTestQuickTask))]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class MsBuildDailyTask
    : FrostingTask<BuildContext>
{
}
