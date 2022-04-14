using Build.Utils;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CreateSonarQubeDirectory")]
public class CreateSonarQubeDirectoryTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (context.DirectoryExists(Constants.SonarQubePath))
        {
            return;
        }

        context.CreateDirectory(Constants.SonarQubePath);
    }
}
