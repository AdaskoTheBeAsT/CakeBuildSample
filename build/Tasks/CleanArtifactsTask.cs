using Build.Utils;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CleanArtifacts")]
public class CleanArtifactsTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.DirectoryExists(Constants.ArtifactsPath))
        {
            return;
        }

        context.DeleteDirectory(
            Constants.ArtifactsPath,
            new DeleteDirectorySettings
            {
                Force = true,
                Recursive = true,
            });
    }
}
