using Cake.Frosting;
using Cake.Sonar;

namespace Build.Tasks;

[TaskName("SonarEnd")]
public class SonarEndTask
    : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var settings = new SonarEndSettings
        {
            UseCoreClr = true,
            Login = context.SonarToken,
            EnvironmentVariables =
                {
                    ["DOTNET_ROLL_FORWARD"] = "Major",
                },
        };

        context.SonarEnd(settings);
    }
}
