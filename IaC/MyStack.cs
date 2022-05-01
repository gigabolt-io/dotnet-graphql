using Pulumi;
using Pulumi.DigitalOcean;
using Pulumi.DigitalOcean.Inputs;

namespace IaC
{
    class MyStack : Stack
    {
        public MyStack()
        {
            var config = new Pulumi.Config();
            var apiKey = config.Require("apiKey");
        
            var app = new App("app", new AppArgs
            {
                Spec = new AppSpecArgs
                {
                    Name = "job-search",
                    Region = "nyc3",
                    Services = new AppSpecServiceArgs
                    {
                        Name = "job-search-api",
                        Github = new AppSpecServiceGithubArgs
                        {
                            DeployOnPush = true,
                            Branch = "master",
                            Repo = "gigabolt.io/job-search-api",
                        },
                        RunCommand = "dotnet run",
                        HttpPort = 80,
                        Routes = new InputList<AppSpecServiceRouteArgs>
                        {
                            new AppSpecServiceRouteArgs
                            {
                                Path = "/api",
                                PreservePathPrefix = true
                            }
                        },
                        InstanceCount = 1,
                        InstanceSizeSlug = "basic-xxs",
                        Envs = new InputList<AppSpecServiceEnvArgs>
                        {
                            new AppSpecServiceEnvArgs  {
                                Key = "API_KEY",
                                Value = apiKey,
                            }
                        },
                   

                    }
                }
            });
        
            Endpoint = app.LiveUrl;
        }

        [Output]
        public Output<string> Endpoint { get; set; }
    }
}
