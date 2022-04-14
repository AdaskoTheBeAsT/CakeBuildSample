using System;
using Build.Utils;
using Cake.Common.IO;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("CleanObjBin")]
public class CleanObjBinTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var masks = new[]
        {
            $"{Constants.SolutionDirectory}/**/obj",
            $"{Constants.SolutionDirectory}/**/bin",
        };

        var deleteDirectorySettings = new DeleteDirectorySettings
        {
            Force = true,
            Recursive = true,
        };

        foreach (var mask in masks)
        {
            var globPattern = new GlobPattern(mask);
            var directories = context.GetDirectories(globPattern, new GlobberSettings
            {
                Predicate = (dir) =>
                    !dir.Path.FullPath.Contains("/build/", StringComparison.OrdinalIgnoreCase)
                    && !dir.Path.FullPath.Contains("\\build\\", StringComparison.OrdinalIgnoreCase)
                    && !dir.Path.FullPath.Contains("node_modules", StringComparison.OrdinalIgnoreCase),
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
}
