using Build.Utils;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CleanSonarQube")]
public class CleanSonarQubeTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.DirectoryExists(Constants.SonarQubePath))
        {
            return;
        }

        context.DeleteDirectory(
            Constants.SonarQubePath,
            new DeleteDirectorySettings
            {
                Force = true,
                Recursive = true,
            });
    }
}
