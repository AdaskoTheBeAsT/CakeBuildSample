using System;
using System.IO;
using System.Linq;
using Cake.Core.IO;

namespace Build.Utils;

public static class NuGetPackagesLocator
{
    public static DirectoryPath GetNuGetPackagesDirectoryPath(BuildContext context)
    {
        if (context.LocalNuGetPackages)
        {
            return new DirectoryPath(Constants.MainPath)
                .Combine(new DirectoryPath("packages"))
                .MakeAbsolute(context.Environment);
        }

        var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return new DirectoryPath(userProfile)
            .Combine(new DirectoryPath(".nuget/packages"));
    }

    public static IDirectory GetNugetPackageLatestVersionDirectoryPath(
        BuildContext context,
        string packageName)
    {
        var packagesDirectoryPath = GetNuGetPackagesDirectoryPath(context);
        var xunitPackagePath = packagesDirectoryPath
            .Combine(new DirectoryPath(packageName));
        var xunitPackageDirectory = context.FileSystem.GetDirectory(xunitPackagePath);
        var versionDirectories = xunitPackageDirectory.GetDirectories("*", SearchScope.Current).ToList();
        if (versionDirectories == null || versionDirectories.Count == 0)
        {
            throw new DirectoryNotFoundException();
        }

        return versionDirectories.OrderByDescending(s => s.Path.FullPath).First();
    }
}
