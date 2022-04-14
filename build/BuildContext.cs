using System;
using System.Collections.Generic;
using System.Linq;
using Build.Exceptions;
using Build.Utils;
using Cake.Common.Solution;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build;

public class BuildContext
    : FrostingContext
{
    public BuildContext(ICakeContext context)
        : base(context)
    {
        BuildConfiguration = context.Arguments.GetArgument("Configuration") ?? "Debug";
        LocalNuGetPackages = context.Arguments.GetArgument(nameof(LocalNuGetPackages))?
            .Equals("true", StringComparison.OrdinalIgnoreCase)
            ?? false;
        SolutionFilePath = new FilePath(Constants.SolutionPath).MakeAbsolute(context.Environment);
        SonarProjectKey = context.Arguments.GetArgument(nameof(SonarProjectKey)) ?? "<url>";
        SonarUrl = context.Arguments.GetArgument(nameof(SonarUrl)) ?? "http://localhost:9000";
        SonarToken = context.Arguments.GetArgument(nameof(SonarToken)) ?? string.Empty;

        var solutionParser = new SolutionParser(context.FileSystem, context.Environment);
        var result = solutionParser.Parse(SolutionFilePath);
        AllProjectPaths = result
            .Projects
            .Where(p => p.Path.FullPath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Path.FullPath, StringComparer.OrdinalIgnoreCase)
            .Select(p => p.Path.MakeAbsolute(context.Environment))
            .ToList();

        AllProjectNames = AllProjectPaths
            .Select(p => p.GetFilenameWithoutExtension().FullPath)
            .ToList();

        UnitTestProjectPaths = AllProjectPaths
            .Where(p => p.FullPath.Contains(".Tests.", StringComparison.OrdinalIgnoreCase))
            .ToList();

        UnitTestProjectNames = UnitTestProjectPaths
            .Select(p => p.GetFilenameWithoutExtension().FullPath)
            .ToList();

        IntegrationTestProjectPaths = AllProjectPaths
            .Where(p => p.FullPath.Contains(".IntegrationTests.", StringComparison.OrdinalIgnoreCase))
            .ToList();

        IntegrationTestProjectNames = IntegrationTestProjectPaths
            .Select(p => p.GetFilenameWithoutExtension().FullPath)
            .ToList();
    }

    public IList<string> AllProjectNames { get; set; }

    public IList<FilePath> AllProjectPaths { get; set; }

    public string BuildConfiguration { get; set; }

    public IList<string> IntegrationTestProjectNames { get; set; }

    public IList<FilePath> IntegrationTestProjectPaths { get; set; }

    public bool LocalNuGetPackages { get; set; }

    public FilePath SolutionFilePath { get; set; }

    public string SonarProjectKey { get; set; }

    public string SonarUrl { get; set; }

    public string SonarToken { get; set; }

    public IList<string> UnitTestProjectNames { get; set; }

    public IList<FilePath> UnitTestProjectPaths { get; set; }
}
