using System;
using System.IO;
using System.Text.RegularExpressions;
using Build.Utils;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("AdjustFrontendCoverageReport")]
public class AdjustFrontendCoverageReportTask
    : FrostingTask<BuildContext>
{
    private readonly Regex _regex;

    public AdjustFrontendCoverageReportTask()
    {
        _regex = new Regex(
            "(?<first>\\s+[<]package\\sname=[\"])(?<middle>[^\"]+)(?<last>[\"].*)",
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(5));
    }

    public override void Run(BuildContext context)
    {
        var frontendUnitReportPath = new DirectoryPath(Constants.FrontendUnitTestPath)
            .MakeAbsolute(context.Environment)
            .Combine(Constants.FrontendUiPath)
            .Combine(Constants.CoverageDirectoryName);
        var frontendCoverageFilePath = frontendUnitReportPath
            .CombineWithFilePath(Constants.CoverageCoberturaFileName);
        var frontendCoverageAdjustedFilePath = frontendUnitReportPath
            .CombineWithFilePath(Constants.CoverageCoberturaFileName);

        if (!File.Exists(frontendCoverageFilePath.FullPath))
        {
            return;
        }

#pragma warning disable SCS0018 // Potential Path Traversal vulnerability
#pragma warning disable SEC0116 // Unvalidated file paths are passed to a file read API
        if (File.Exists(frontendCoverageAdjustedFilePath.FullPath))
        {
            File.Delete(frontendCoverageAdjustedFilePath.FullPath);
        }

        using (var fs = File.OpenRead(frontendCoverageAdjustedFilePath.FullPath))
        using (var sw = new StreamWriter(fs))
        {
            foreach (var line in File.ReadAllLines(frontendCoverageFilePath.FullPath))
            {
                var match = _regex.Match(line);
                if (!match.Success)
                {
                    sw.WriteLine(line);
                    continue;
                }

                var first = match.Groups["first"].Value;
                var middle = match.Groups["middle"].Value;
                middle = middle.Replace(".", "/", StringComparison.OrdinalIgnoreCase);
                var last = match.Groups["last"].Value;
                sw.WriteLine($"{first}frontend/apps/ui/{middle}{last}");
            }
        }
#pragma warning restore SEC0116 // Unvalidated file paths are passed to a file read API
#pragma warning restore SCS0018 // Potential Path Traversal vulnerability
    }
}
