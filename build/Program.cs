using System;
using Cake.Frosting;

namespace Build;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .InstallTool(new Uri("nuget:?package=dotnet-reportgenerator-globaltool&version=5.1.4&loaddependencies=true"))
            .InstallTool(new Uri("nuget:?package=dotnet-sonarscanner&version=5.5.3&loaddependencies=true"))
            .InstallTool(new Uri("nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2021.3.4&loaddependencies=true"))
            .InstallTool(new Uri("nuget:?package=NuGet.CommandLine&version=6.1.0&loaddependencies=true"))
            .InstallTool(new Uri("nuget:?package=ReSharperReports&version=0.4.0&loaddependencies=true"))
            .InstallTool(new Uri("nuget:?package=xunit.runner.console&version=2.4.1&loaddependencies=true"))
            .Run(args);
    }
}
