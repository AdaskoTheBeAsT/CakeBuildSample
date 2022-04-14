using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Clean")]
[IsDependentOn(typeof(CleanObjBinTask))]
[IsDependentOn(typeof(CleanArtifactsTask))]
[IsDependentOn(typeof(CleanSonarQubeTask))]
[IsDependentOn(typeof(CleanReportsTask))]
[IsDependentOn(typeof(CleanNxCacheTask))]
public class CleanTask
    : FrostingTask<BuildContext>
{
}
