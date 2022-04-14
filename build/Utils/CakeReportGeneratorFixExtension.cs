using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Execute;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core;
using Cake.Core.IO;

namespace Build.Utils;

/// <summary>
/// To be removed after issue will be solved.
/// https://github.com/cake-build/cake/issues/3864.
/// </summary>
public static class CakeReportGeneratorFixExtension
{
    public static void ReportGenerator2(
        this BuildContext context,
        GlobPattern pattern,
        DirectoryPath targetDir,
        ReportGeneratorSettings settings)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var reports = context.Globber.GetFiles(pattern);
        context.ReportGenerator2(reports, targetDir, settings);
    }

    public static void ReportGenerator2(
        this BuildContext context,
        FilePath filePath,
        DirectoryPath targetDir,
        ReportGeneratorSettings settings)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.ReportGenerator2(new[] { filePath }, targetDir, settings);
    }

    public static void ReportGenerator2(
        this BuildContext context,
        IEnumerable<FilePath> reports,
        DirectoryPath targetDir,
        ReportGeneratorSettings settings)
    {
        var reportGeneratorFilePath = ReportGeneratorLocator.GetReportGeneratorFilePath(context);
        var argumentBuilder = GetArgument(
            context,
            settings,
            reports,
            targetDir);

        var dotNetExecuteSettings = new DotNetExecuteSettings
        {
            FrameworkVersion = "6.0.4",
        };

        context.DotNetExecute(
            reportGeneratorFilePath,
            argumentBuilder,
            dotNetExecuteSettings);
    }

    private static ProcessArgumentBuilder GetArgument(
        BuildContext context,
        ReportGeneratorSettings settings,
        IEnumerable<FilePath> reports,
        DirectoryPath targetDir)
    {
        var builder = new ProcessArgumentBuilder();

        var joinedReports = string.Join(";", reports.Select(r => r.MakeAbsolute(context.Environment).FullPath));

#pragma warning disable CC0021
        AppendQuoted(builder, "reports", joinedReports);
#pragma warning restore CC0021

        AppendQuoted(builder, "targetdir", targetDir.MakeAbsolute(context.Environment).FullPath);

        if (settings.ReportTypes != null && settings.ReportTypes.Any())
        {
            var joined = string.Join(";", settings.ReportTypes);
            AppendQuoted(builder, "reporttypes", joined);
        }

        if (settings.SourceDirectories != null && settings.SourceDirectories.Any())
        {
            var joined = string.Join(";", settings.SourceDirectories.Select(d => d.MakeAbsolute(context.Environment).FullPath));
            AppendQuoted(builder, "sourcedirs", joined);
        }

        if (settings.HistoryDirectory != null)
        {
            AppendQuoted(builder, "historydir", settings.HistoryDirectory.MakeAbsolute(context.Environment).FullPath);
        }

        if (settings.AssemblyFilters != null && settings.AssemblyFilters.Any())
        {
            var joined = string.Join(";", settings.AssemblyFilters);
            AppendQuoted(builder, "assemblyfilters", joined);
        }

        if (settings.ClassFilters != null && settings.ClassFilters.Any())
        {
            var joined = string.Join(";", settings.ClassFilters);
            AppendQuoted(builder, "classfilters", joined);
        }

        if (settings.Verbosity != null)
        {
            AppendQuoted(builder, "verbosity", settings.Verbosity?.ToString() ?? string.Empty);
        }

        return builder;
    }

    private static void AppendQuoted(ProcessArgumentBuilder builder, string key, string value) =>
        builder.AppendQuoted(string.Format(CultureInfo.InvariantCulture, "-{0}:{1}", key, value));
}
