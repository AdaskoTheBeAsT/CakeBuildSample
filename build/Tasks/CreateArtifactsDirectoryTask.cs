using Build.Utils;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CreateArtifactsDirectory")]
public class CreateArtifactsDirectoryTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (context.DirectoryExists(Constants.ArtifactsPath))
        {
            return;
        }

        context.CreateDirectory(Constants.ArtifactsPath);
    }
}
