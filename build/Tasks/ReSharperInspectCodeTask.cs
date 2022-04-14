using System;
using System.Collections.Generic;
using Build.Utils;
using Cake.Common.Tools.InspectCode;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("ReSharperInspectCode")]
public class ReSharperInspectCodeTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var inspectCodeXmlReportPath = new DirectoryPath(Constants.ReportsPath)
            .MakeAbsolute(context.Environment)
            .CombineWithFilePath(Constants.ReSharperInspectCodeXmlReportName);

        var settings = new InspectCodeSettings
        {
            SolutionWideAnalysis = true,
            MsBuildProperties = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
            OutputFile = inspectCodeXmlReportPath,
            ThrowExceptionOnFindingViolations = false,
        };

        context.InspectCode(context.SolutionFilePath, settings);
    }
}
