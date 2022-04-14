using System.Collections.Generic;
using Build.Utils;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Sonar;

namespace Build.Tasks;

[TaskName("SonarBegin")]
public class SonarBeginTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var solutionDirectory = context.SolutionFilePath.GetDirectory();

        var settings = new SonarBeginSettings
        {
            UseCoreClr = true,
            Key = context.SonarProjectKey,
            Url = context.SonarUrl,
            Login = context.SonarToken,
            OpenCoverReportsPath = GetUnitTestCoverageReportPath(context),

            OpenCoverIntegrationReportsPath = GetIntegrationTestCoverageReportPath(context),
            XUnitReportsPath = GetTestReportPath(context),
            Exclusions = GetExclusions(),
            EnvironmentVariables =
                {
                    ["DOTNET_ROLL_FORWARD"] = "Major",
                },
            ProjectBaseDir = solutionDirectory.FullPath,
            ArgumentCustomization =
                args =>
                    args.AppendSwitch("/d:sonar.scm.disabled", "=", "true"),
        };

        context.SonarBegin(settings);
    }

    private static string GetExclusions()
    {
        var exclusions = new[]
        {
            "*Designer.cs",
            "**/App_Data/*",
            "**/bin/**/*",
            "**/obj/**/*",
            "**/appsettings*.json",
            "**/launchSettings.json",
            "**/Dockerfile",
            "**/*.feature",
            "**/build/**/*",
        };

        return string.Join(",", exclusions);
    }

    private static string GetIntegrationTestCoverageReportPath(BuildContext context) =>
        new DirectoryPath(Constants.BackendIntegrationTestPath)
            .MakeAbsolute(context.Environment)
            .Combine("**")
            .CombineWithFilePath(Constants.CoverageOpenCoverFileName)
            .FullPath;

    private static string GetUnitTestCoverageReportPath(BuildContext context) =>
        new DirectoryPath(Constants.BackendUnitTestPath)
            .MakeAbsolute(context.Environment)
            .Combine("**")
            .CombineWithFilePath(Constants.CoverageOpenCoverFileName)
            .FullPath;

    private static string GetTestReportPath(BuildContext context)
    {
        var backendUnitTestPath = new DirectoryPath(Constants.BackendUnitTestPath)
            .MakeAbsolute(context.Environment);

        var backendIntegrationUnitTestPath = new DirectoryPath(Constants.BackendIntegrationTestPath)
            .MakeAbsolute(context.Environment);

        var xunitReportPaths = new List<string>();
        foreach (var unitTestProjectName in context.UnitTestProjectNames)
        {
            xunitReportPaths.Add($"{backendUnitTestPath.FullPath}/{unitTestProjectName}.xunit.xml");
        }

        foreach (var integrationTestProjectName in context.IntegrationTestProjectNames)
        {
            xunitReportPaths.Add($"{backendIntegrationUnitTestPath.FullPath}/{integrationTestProjectName}.xunit.xml");
        }

        return string.Join(",", xunitReportPaths);
    }
}
