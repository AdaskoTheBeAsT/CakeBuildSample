using Build.Utils;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CreateReportsDirectory")]
public class CreateReportsDirectoryTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (context.DirectoryExists(Constants.ReportsPath))
        {
            return;
        }

        context.CreateDirectory(Constants.ReportsPath);

        if (context.DirectoryExists(Constants.BackendUnitTestPath))
        {
            return;
        }

        context.CreateDirectory(Constants.BackendUnitTestPath);

        if (context.DirectoryExists(Constants.BackendIntegrationTestPath))
        {
            return;
        }

        context.CreateDirectory(Constants.BackendIntegrationTestPath);
    }
}
