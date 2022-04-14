using Build.Utils;
using Cake.Frosting;
using Cake.Yarn;

namespace Build.Tasks;

[TaskName("YarnRunTestAll")]
public class YarnRunTestAllTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context) =>
        context.Yarn()
            .FromPath(Constants.FrontendPath)
            .RunScript("test:all");
}
