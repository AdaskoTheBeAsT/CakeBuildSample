using Cake.Frosting;
using Cake.Npm;
using Cake.Npm.Install;

namespace Build.Tasks;

[TaskName("NpmInstallGlobal")]
public class NpmInstallGlobalTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.NpmInstall(new NpmInstallSettings
        {
            Global = true,
            Production = false,
            Packages = { "yarn", "nx", "typescript", "jest", "npm-check-updates", "cross-env", "shx" },
        });
    }
}
