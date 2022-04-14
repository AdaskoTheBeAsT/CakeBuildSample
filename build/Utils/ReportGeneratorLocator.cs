using System.Linq;
using Build.Exceptions;
using Cake.Core;
using Cake.Core.IO;

namespace Build.Utils;

public static class ReportGeneratorLocator
{
    public static FilePath GetReportGeneratorFilePath(BuildContext context)
    {
        var toolPath = context.Configuration.GetToolPath(
            context.Environment.WorkingDirectory,
            context.Environment);
        var toolDirectory = context.FileSystem.GetDirectory(toolPath);
        var versionDirectories = toolDirectory.GetDirectories(
            "dotnet-reportgenerator-globaltool.*",
            SearchScope.Current)
            .ToList();
        if (versionDirectories == null || versionDirectories.Count == 0)
        {
            throw new ReportGeneratorNotFoundException();
        }

        var lastVersion = versionDirectories.OrderByDescending(s => s.Path.FullPath).First();
        var directoryPath = lastVersion.Path.Combine(new DirectoryPath("tools/net6.0/any"));
        return directoryPath.CombineWithFilePath(new FilePath("ReportGenerator.dll"));
    }
}
