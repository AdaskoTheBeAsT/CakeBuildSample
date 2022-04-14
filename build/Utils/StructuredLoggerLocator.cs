using System.Linq;
using Build.Exceptions;
using Cake.Core.IO;

namespace Build.Utils;

public static class StructuredLoggerLocator
{
    public static FilePath GetStructuredLoggerFilePath(BuildContext context)
    {
        var lastVersion = NuGetPackagesLocator.GetNugetPackageLatestVersionDirectoryPath(
            context,
            "cake.issues.msbuild");
        var directoryPath = lastVersion.Path.Combine(new DirectoryPath("lib/netstandard2.0"));
        return directoryPath.CombineWithFilePath(new FilePath("StructuredLogger.dll"));
    }
}
