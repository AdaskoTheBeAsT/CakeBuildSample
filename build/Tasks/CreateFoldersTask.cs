using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CreateFolders")]
[IsDependentOn(typeof(CreateArtifactsDirectoryTask))]
[IsDependentOn(typeof(CreateReportsDirectoryTask))]
[IsDependentOn(typeof(CreateSonarQubeDirectoryTask))]
public class CreateFoldersTask
    : FrostingTask<BuildContext>
{
}
