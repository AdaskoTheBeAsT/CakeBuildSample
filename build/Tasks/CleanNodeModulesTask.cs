using System;
using Build.Utils;
using Cake.Common.IO;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CleanNodeModules")]
public class CleanNodeModulesTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var deleteDirectorySettings = new DeleteDirectorySettings
        {
            Force = true,
            Recursive = true,
        };

        var globPattern = new GlobPattern($"{Constants.SolutionDirectory}/**/node_modules");
        var directories = context.GetDirectories(globPattern, new GlobberSettings
        {
            Predicate = (dir) =>
                !dir.Path.FullPath.Contains("/build/", StringComparison.OrdinalIgnoreCase)
                && !dir.Path.FullPath.Contains("\\build\\", StringComparison.OrdinalIgnoreCase),
        });

        if (directories == null || directories.Count == 0)
        {
            return;
        }

        foreach (var dir in directories)
        {
            if (!context.DirectoryExists(dir))
            {
                continue;
            }

            context.DeleteDirectory(dir, deleteDirectorySettings);
        }
    }
}
