using System.Collections.Generic;
using Build.Utils;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Issues;
using Cake.Issues.DupFinder;
using Cake.Issues.InspectCode;
using Cake.Issues.MsBuild;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Generic;

namespace Build.Tasks;

[TaskName("BuildReport")]
public class BuildReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var reportsPath = new DirectoryPath(Constants.ReportsPath)
            .MakeAbsolute(context.Environment);
        var msBuildBinaryLogFilePath = reportsPath.CombineWithFilePath(Constants.MsBuildBinLogFileName);
        var issuesFilePath = reportsPath.CombineWithFilePath(Constants.IssuesReportName);

        var inspectCodeXmlFilePath = reportsPath.CombineWithFilePath(Constants.ReSharperInspectCodeXmlReportName);
        var inspectCodeIssuesSettings = new InspectCodeIssuesSettings(inspectCodeXmlFilePath);
        var issuesSettings = new MsBuildIssuesSettings(
            msBuildBinaryLogFilePath,
            context.MsBuildBinaryLogFileFormat());
        context.CreateIssueReport(
            new List<IIssueProvider>
            {
                context.InspectCodeIssues(inspectCodeIssuesSettings),
                context.MsBuildIssues(issuesSettings),
            },
            context.GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => settings.WithOption(
                    HtmlDxDataGridOption.Theme,
                    DevExtremeTheme.MaterialBlueLight)),
            Constants.SolutionPath,
            issuesFilePath);
    }
}
