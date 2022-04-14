using Build.Utils;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("DotNetBuildDb")]
[IsDependentOn(typeof(KillDotNetBuildServerTask))]
public class DotNetBuildDbTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var structuredLoggerFilePath = StructuredLoggerLocator.GetStructuredLoggerFilePath(context);
        var msBuildBinaryLogFilePath = new DirectoryPath(Constants.ReportsPath)
            .CombineWithFilePath(Constants.MsBuildDbBinLogFileName);
        var dbTargetsPath = new DirectoryPath("./ms-mssql.sql-database-projects-vscode-0.14.1/BuildDirectory")
            .MakeAbsolute(context.Environment);

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
            NoRestore = false,
            MSBuildSettings = dotNetCoreMsBuildSettings,
            Verbosity = DotNetVerbosity.Normal,
            ArgumentCustomization =
                args =>
                    args
                        .AppendSwitch("/p:NetCoreBuild", "=", "true")
                        .AppendSwitch("/p:NETCoreTargetsPath", "=", dbTargetsPath.FullPath),
        };

        context.DotNetBuild(Constants.SolutionPath, dotNetCoreBuildSettings);
    }
}
