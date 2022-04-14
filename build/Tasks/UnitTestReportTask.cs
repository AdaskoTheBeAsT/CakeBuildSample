using System;
using System.Xml;
using Build.Utils;
using Cake.Common.Xml;
using Cake.Core.IO;
using Cake.Frosting;

namespace Build.Tasks;

[TaskName("UnitTestReport")]
public class UnitTestReportTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var xunitPath = context.Tools.Resolve("xunit.console.dll");
        var nunitXsltOutputPath = XUnitXsltExtractor.ExtractXslt(context, xunitPath, "Xunit.ConsoleClient.NUnitXml.xslt");
        var htmlXsltOutputPath = XUnitXsltExtractor.ExtractXslt(context, xunitPath, "Xunit.ConsoleClient.HTML.xslt");

        var unitTestReportPath = new DirectoryPath(Constants.BackendUnitTestPath)
            .MakeAbsolute(context.Environment);

        var xmlTransformationSettings = new XmlTransformationSettings
        {
            Indent = true,
            IndentChars = "    ",
            NewLineHandling = NewLineHandling.Replace,
            NewLineChars = Environment.NewLine,
            Overwrite = true,
        };

        foreach (var name in context.UnitTestProjectNames)
        {
            var xmlXunitPath = unitTestReportPath.CombineWithFilePath($"{name}.xunit.xml");
            var xmlNunitPath = unitTestReportPath.CombineWithFilePath($"{name}.nunit.xml");
            var htmlPath = unitTestReportPath.CombineWithFilePath($"{name}.html");
            context.XmlTransform(nunitXsltOutputPath, xmlXunitPath, xmlNunitPath, xmlTransformationSettings);
            context.XmlTransform(htmlXsltOutputPath, xmlXunitPath, htmlPath, xmlTransformationSettings);
        }
    }
}
