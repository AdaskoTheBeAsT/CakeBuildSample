using Build.Utils;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Common.Tools.MSBuild;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("MsBuild")]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class MsBuildTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var structuredLoggerFilePath = StructuredLoggerLocator.GetStructuredLoggerFilePath(context);
        var msBuildBinaryLogFilePath = new DirectoryPath(Constants.ReportsPath)
            .CombineWithFilePath(Constants.MsBuildBinLogFileName);

        var msBuildSettings = new MSBuildSettings
        {
            Restore = false,
        }
            .WithProperty("DebugType", "pdbonly")
            .WithProperty("TreatWarningsAsErrors", "false")
            .WithTarget(nameof(Build))
            .SetMaxCpuCount(0)
            .SetConfiguration(context.BuildConfiguration)
            .SetNodeReuse(reuse: false)
            .SetVerbosity(Verbosity.Verbose)
            .WithLogger(
                structuredLoggerFilePath.FullPath,
                "BinaryLogger",
                msBuildBinaryLogFilePath.FullPath);

        context.MSBuild(Constants.SolutionPath, msBuildSettings);
    }
}
