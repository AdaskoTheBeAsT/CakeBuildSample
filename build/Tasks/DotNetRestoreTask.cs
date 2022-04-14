using Build.Utils;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Restore;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("DotNetRestore")]
public class DotNetRestoreTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var dotNetCoreRestoreSettings = new DotNetRestoreSettings
        {
            ConfigFile = Constants.NuGetConfigRelativePath,
        };

        context.DotNetRestore(Constants.SolutionDirectory, dotNetCoreRestoreSettings);
    }
}
