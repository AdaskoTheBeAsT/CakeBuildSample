using Build.Utils;
using Cake.Common.Tools.NuGet;
using Cake.Common.Tools.NuGet.Restore;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("NuGetRestore")]
public class NuGetRestoreTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var nuGetRestoreSettings = new NuGetRestoreSettings
        {
            ConfigFile = Constants.NuGetConfigRelativePath,
        };

        context.NuGetRestore(Constants.SolutionPath, nuGetRestoreSettings);
    }
}
