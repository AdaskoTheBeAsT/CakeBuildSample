using System;
using System.Xml;
using Build.Utils;
using Cake.Common.Xml;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("IntegrationTestReport")]
public class IntegrationTestReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var xunitPath = context.Tools.Resolve("xunit.console.dll");
        var nunitXsltOutputPath = XUnitXsltExtractor.ExtractXslt(context, xunitPath, "Xunit.ConsoleClient.NUnitXml.xslt");
        var htmlXsltOutputPath = XUnitXsltExtractor.ExtractXslt(context, xunitPath, "Xunit.ConsoleClient.HTML.xslt");

        var integrationReportPath = new DirectoryPath(Constants.BackendIntegrationTestPath)
            .MakeAbsolute(context.Environment);

        var xmlTransformationSettings = new XmlTransformationSettings
        {
            Indent = true,
            IndentChars = "    ",
            NewLineHandling = NewLineHandling.Replace,
            NewLineChars = Environment.NewLine,
            Overwrite = true,
        };

        foreach (var name in context.IntegrationTestProjectNames)
        {
            var xmlXunitPath = integrationReportPath.CombineWithFilePath($"{name}.xunit.xml");
            var xmlNunitPath = integrationReportPath.CombineWithFilePath($"{name}.nunit.xml");
            var htmlPath = integrationReportPath.CombineWithFilePath($"{name}.html");
            context.XmlTransform(nunitXsltOutputPath, xmlXunitPath, xmlNunitPath, xmlTransformationSettings);
            context.XmlTransform(htmlXsltOutputPath, xmlXunitPath, htmlPath, xmlTransformationSettings);
        }
    }
}
