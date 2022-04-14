using Build.Utils;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Test;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("IntegrationTest")]
public class IntegrationTestTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var integrationReportPath = new DirectoryPath(Constants.BackendIntegrationTestPath)
            .MakeAbsolute(context.Environment);

        var dotNetCoreTestSettings = new DotNetTestSettings
        {
            Configuration = context.BuildConfiguration,
            Framework = Constants.Framework,
            NoBuild = true,
            NoRestore = true,
            Filter = "FullyQualifiedName~IntegrationTest",
            Collectors = new[] { "XPlat Code Coverage" },
            Loggers = new[] { $"\\\"xunit;LogFilePath={integrationReportPath.FullPath}/{{assembly}}.xunit.xml\\\"" },
            TestAdapterPath = ".",
            ResultsDirectory = integrationReportPath.FullPath,
            ArgumentCustomization =
                args =>
                    args.Append("-- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=cobertura,opencover"),
        };

        context.DotNetTest(Constants.SolutionPath, dotNetCoreTestSettings);
    }
}
