using Build.Utils;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Test;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("UnitTestQuick")]
public class UnitTestQuickTask
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
            Filter = "FullyQualifiedName!~IntegrationTest&FullyQualifiedName!~PerformanceTest",
            TestAdapterPath = ".",
        };

        context.DotNetTest(Constants.SolutionPath, dotNetCoreTestSettings);
    }
}
