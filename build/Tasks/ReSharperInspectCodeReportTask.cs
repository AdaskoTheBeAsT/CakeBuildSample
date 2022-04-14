using Build.Utils;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.ReSharperReports;

namespace Build.Tasks;

[TaskName("ReSharperInspectCodeReport")]
public class ReSharperInspectCodeReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var reportsPath = new DirectoryPath(Constants.ReportsPath).MakeAbsolute(context.Environment);
        var inspectCodeXmlReportPath = reportsPath.CombineWithFilePath(Constants.ReSharperInspectCodeXmlReportName);
        var inspectCodeHtmlReportPath = reportsPath.CombineWithFilePath(Constants.ReSharperInspectCodeHtmlReportName);
        context.ReSharperReports(inspectCodeXmlReportPath, inspectCodeHtmlReportPath);
    }
}
