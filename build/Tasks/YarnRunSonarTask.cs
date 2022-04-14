using Build.Utils;
using Cake.Frosting;
using Cake.Yarn;

namespace Build.Tasks;

[TaskName("YarnRunSonar")]
public class YarnRunSonarTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context) =>
        context.Yarn()
            .FromPath(Constants.FrontendPath)
            .RunScript("build --prod");
}
