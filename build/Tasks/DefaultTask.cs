using Cake.Frosting;

namespace Build.Tasks;

[TaskName("Default")]
[IsDependentOn(typeof(DotNetBuildNightlyTask))]
public class DefaultTask
    : FrostingTask<BuildContext>
{
}
