using Build.Utils;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Test;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("IntegrationTestQuick")]
public class IntegrationTestQuickTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var dotNetCoreTestSettings = new DotNetTestSettings
        {
            Configuration = context.BuildConfiguration,
            Framework = Constants.Framework,
            NoBuild = true,
            NoRestore = true,
            Filter = "FullyQualifiedName~IntegrationTest",
            TestAdapterPath = ".",
        };

        context.DotNetTest(Constants.SolutionPath, dotNetCoreTestSettings);
    }
}
