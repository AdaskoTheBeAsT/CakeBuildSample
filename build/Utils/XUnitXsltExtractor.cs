using System.IO;
using System.Reflection;
using Build.Exceptions;
using Cake.Core.IO;

namespace Build.Utils;

public static class XUnitXsltExtractor
{
#pragma warning disable SCS0018 // Potential Path Traversal vulnerability was found where '{0}' in '{1}' may be tainted by user-controlled data from '{2}' in method '{3}'.
#pragma warning disable SEC0112 // Unvalidated file paths are passed to a FileStream API, which can allow unauthorized file system operations (e.g. read, write, delete) to be performed on unintended server files.
#pragma warning disable S3885 // Replace this call to 'Assembly.LoadFrom' with 'Assembly.Load'.
    public static FilePath ExtractXslt(BuildContext context, FilePath xunitPath, string name)
    {
        var xsltOutputPath = new DirectoryPath(Constants.ReportsPath)
            .CombineWithFilePath(new FilePath(name))
            .MakeAbsolute(context.Environment);

        using (var resource = Assembly.LoadFrom(xunitPath.FullPath).GetManifestResourceStream(name))
        using (var file = new FileStream(xsltOutputPath.FullPath, FileMode.Create, FileAccess.Write))
        {
            if (resource is null)
            {
                throw new XUnitResourceNullException();
            }

            resource.CopyTo(file);
        }

        return xsltOutputPath;
    }
#pragma warning restore S3885 // Replace this call to 'Assembly.LoadFrom' with 'Assembly.Load'.
#pragma warning restore SEC0112 // Unvalidated file paths are passed to a FileStream API, which can allow unauthorized file system operations (e.g. read, write, delete) to be performed on unintended server files.
#pragma warning restore SCS0018 // Potential Path Traversal vulnerability was found where '{0}' in '{1}' may be tainted by user-controlled data from '{2}' in method '{3}'.

}
