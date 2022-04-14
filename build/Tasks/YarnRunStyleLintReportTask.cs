using System;
using Build.Utils;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Cake.Yarn;

namespace Build.Tasks;

[TaskName("YarnRunStyleLintReport")]
public class YarnRunStyleLintReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        try
        {
            context.Yarn()
                .FromPath(Constants.FrontendPath)
                .RunScript("htmlhint:report");
        }
        catch (Exception ex)
        {
            context.Log.Information(ex.Message);
        }
    }
}
