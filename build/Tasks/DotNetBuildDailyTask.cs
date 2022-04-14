using Cake.Frosting;

namespace Build.Tasks;

[TaskName("DotNetBuildDaily")]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(CreateFoldersTask))]
[IsDependentOn(typeof(DotNetRestoreTask))]
[IsDependentOn(typeof(DotNetBuildQuickTask))]
[IsDependentOn(typeof(UnitTestQuickTask))]
[IsDependentOn(typeof(IntegrationTestQuickTask))]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class DotNetBuildDailyTask
    : FrostingTask<BuildContext>
{
}
