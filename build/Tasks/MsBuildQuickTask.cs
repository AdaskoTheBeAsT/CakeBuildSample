using Build.Utils;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("MsBuildQuick")]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class MsBuildQuickTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var msBuildSettings = new MSBuildSettings
        {
            Restore = false,
            Verbosity = Verbosity.Minimal,
        }
            .WithProperty("DebugType", "pdbonly")
            .WithProperty("TreatWarningsAsErrors", "false")
            .WithTarget(nameof(Build))
            .SetMaxCpuCount(0)
            .SetConfiguration(context.BuildConfiguration)
            .SetNodeReuse(reuse: false);

        context.MSBuild(Constants.SolutionPath, msBuildSettings);
    }
}
