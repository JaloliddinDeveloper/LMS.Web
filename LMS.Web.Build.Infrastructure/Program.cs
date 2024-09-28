//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

internal class Program
{
    private static void Main(string[] args)
    {

        var aDotNetClient = new ADotNetClient();

        var githubPipeline = new GithubPipeline
        {
            Name = "LMS Web Build Pipeline",

            OnEvents = new Events
            {
                Push = new PushEvent
                {
                    Branches = new string[] { "main" }
                },

                PullRequest = new PullRequestEvent
                {
                    Branches = new string[] { "main" }
                }
            },

            Jobs = new Dictionary<string, Job>
    {
        {
            "build",
            new Job
            {
                RunsOn = BuildMachines.Windows2022,

                Steps = new List<GithubTask>
                {
                    new CheckoutTaskV2
                    {
                        Name = "Check out"
                    },

                    new SetupDotNetTaskV1
                    {
                        Name = "Setup .Net",

                        TargetDotNetVersion = new TargetDotNetVersion
                        {
                            DotNetVersion = "9.0.100-rc.1.24452.12",
                            IncludePrerelease = true
                        }
                    },

                    new RestoreTask
                    {
                        Name = "Restore"
                    },

                    new DotNetBuildTask
                    {
                        Name = "Build"
                    },

                    new TestTask
                    {
                        Name = "Test"
                    }
                }
            }
        }
    }
        };

        string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
        string directoryPath = Path.GetDirectoryName(buildScriptPath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        aDotNetClient.SerializeAndWriteToFile(githubPipeline, path: buildScriptPath);
    }
}