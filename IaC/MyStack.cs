using Pulumi;
using Pulumi.DigitalOcean;
using Pulumi.DigitalOcean.Inputs;
using Pulumi.Docker;

namespace IaC
{
    class MyStack : Stack
    {
        public MyStack()
        {
            var config = new Pulumi.Config();
            var dockerHubPass = config.Require("dockerHubPass");
            var dockerImage = new Image("dockerImage", new ImageArgs
            {
                ImageName = "gigabolt/dotnet-graphql-jobs:latest",
                Build = "../",
                Registry = new ImageRegistry
                {
                    Server = "docker.io",
                    Username = "gigabolt",
                    Password = dockerHubPass
                },
                LocalImageName = "dotnet-graphql-jobs:latest",
            });
            
            ImageUrn = dockerImage.Urn;
            
            
             var apiKey = config.Require("apiKey");
            
             var app = new App("app", new AppArgs
             {
                 Spec = new AppSpecArgs
                 {
                     Name = "job-search",
                     Region = "fra1",
                     Services = new AppSpecServiceArgs
                     {
                         Name = "job-search-api",
                         Image = new AppSpecServiceImageArgs
                         {
                             Registry = "gigabolt",
                             Repository = "dotnet-graphql-jobs",
                             RegistryType = "DOCKER_HUB",
                             Tag = "latest"
                             
                         },
                         SourceDir = "/",
                         HttpPort = 80,
                         Routes = new InputList<AppSpecServiceRouteArgs>
                         {
                             new AppSpecServiceRouteArgs
                             {
                                 Path = "/",
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
        
        [Output]
        public Output<string> ImageUrn { get; set; }
    }
}
