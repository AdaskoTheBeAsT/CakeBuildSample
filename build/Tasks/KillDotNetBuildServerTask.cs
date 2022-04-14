using Cake.Common.Tools.DotNet;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("KillDotNetBuildServer")]
public class KillDotNetBuildServerTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetBuildServerShutdown();
        context.Log.Information(".NET Core Build Server shutdown completed.");
    }
}
