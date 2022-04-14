using Build.Utils;
using Cake.Common.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CleanReports")]
public class CleanReportsTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        if (!context.DirectoryExists(Constants.ReportsPath))
        {
            return;
        }

        context.DeleteDirectory(
            Constants.ReportsPath,
            new DeleteDirectorySettings
            {
                Force = true,
                Recursive = true,
            });
    }
}
