using Cake.Core.IO;

namespace Build.Utils;

public static class XUnitXmlTestLoggerLocator
{
    public static DirectoryPath GetXUnitXmlTestLoggerDirectoryPath(BuildContext context)
    {
        var lastVersion = NuGetPackagesLocator.GetNugetPackageLatestVersionDirectoryPath(
            context,
            "xunitxml.testlogger");
        return lastVersion.Path.Combine(new DirectoryPath("build/_common"))
            .MakeAbsolute(context.Environment);
    }
}
