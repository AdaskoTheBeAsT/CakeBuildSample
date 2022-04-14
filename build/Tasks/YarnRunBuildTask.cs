using Build.Utils;
using Cake.Frosting;
using Cake.Yarn;

namespace Build.Tasks;

[TaskName("YarnRunBuild")]
public class YarnRunBuildTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context) =>
        context.Yarn()
            .FromPath(Constants.FrontendPath)
            .RunScript(string.Empty);
}
