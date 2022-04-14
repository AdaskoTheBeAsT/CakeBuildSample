using System;
using Build.Utils;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Cake.Yarn;

namespace Build.Tasks;

[TaskName("YarnRunEsLintReport")]
public class YarnRunEsLintReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        try
        {
            context.Yarn()
                .FromPath(Constants.FrontendPath)
                .RunScript("eslint:report");
        }
        catch (Exception ex)
        {
            context.Log.Information(ex.Message);
        }
    }
}
