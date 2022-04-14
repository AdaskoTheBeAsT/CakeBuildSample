using Build.Utils;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("DotNetBuildQuick")]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class DotNetBuildQuickTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var dotNetCoreMsBuildSettings = new DotNetMSBuildSettings()
            .WithProperty("DebugType", "full")
            .WithProperty("TreatWarningsAsErrors", "false")
            .SetMaxCpuCount(0)
            .SetConfiguration(context.BuildConfiguration);

        var dotNetCoreBuildSettings = new DotNetBuildSettings
        {
            Configuration = context.BuildConfiguration,
            NoRestore = true,
            MSBuildSettings = dotNetCoreMsBuildSettings,
        };

        context.DotNetBuild(Constants.SolutionPath, dotNetCoreBuildSettings);
    }
}
