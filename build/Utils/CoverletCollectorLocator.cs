using Cake.Core.IO;

namespace Build.Utils;

public static class CoverletCollectorLocator
{
    public static DirectoryPath GetCoverletCollectorDirectoryPath(BuildContext context)
    {
        var lastVersion = NuGetPackagesLocator.GetNugetPackageLatestVersionDirectoryPath(
            context,
            "coverlet.collector");
        return lastVersion.Path.Combine(new DirectoryPath("build/netstandard1.0"))
            .MakeAbsolute(context.Environment);
    }
}
