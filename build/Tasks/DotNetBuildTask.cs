using Build.Utils;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("DotNetBuild")]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class DotNetBuildTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var structuredLoggerFilePath = StructuredLoggerLocator.GetStructuredLoggerFilePath(context);
        var msBuildBinaryLogFilePath = new DirectoryPath(Constants.ReportsPath)
            .CombineWithFilePath(Constants.MsBuildBinLogFileName);

        var dotNetCoreMsBuildSettings = new DotNetMSBuildSettings()
            .WithProperty("DebugType", "full")
            .WithProperty("TreatWarningsAsErrors", "false")
            .SetMaxCpuCount(0)
            .SetConfiguration(context.BuildConfiguration)
            .WithLogger(
                structuredLoggerFilePath.FullPath,
                "BinaryLogger",
                msBuildBinaryLogFilePath.FullPath);

        var dotNetCoreBuildSettings = new DotNetBuildSettings
        {
            Configuration = context.BuildConfiguration,
            NoRestore = true,
            MSBuildSettings = dotNetCoreMsBuildSettings,
            Verbosity = DotNetVerbosity.Detailed,
        };

        context.DotNetBuild(Constants.SolutionPath, dotNetCoreBuildSettings);
    }
}
