using Build.Utils;
using Cake.Frosting;
using Cake.Yarn;

namespace Build.Tasks;

[TaskName("YarnInstall")]
public class YarnInstallTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context) =>
        context.Yarn()
            .FromPath(Constants.FrontendPath)
            .Install();
}
