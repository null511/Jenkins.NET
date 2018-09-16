using Photon.Framework.Server;
using Photon.NuGet.CorePlugin;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Path = System.IO.Path;

namespace Jenkins.NET.Publishing.Scripts
{
    internal class Publish : IDeployScript
    {
        private NuGetCore nugetCore;

        public IServerDeployContext Context {get; set;}

        
        public async Task RunAsync(CancellationToken token)
        {
            nugetCore = new NuGetCore(Context) {
                ApiKey = Context.ServerVariables["global"]["nuget/apiKey"],
            };
            nugetCore.Initialize();

            var packageDir = Path.Combine(Context.BinDirectory, "PublishPackage");

            var packageFilename = Directory
                .GetFiles(packageDir, "jenkinsnet.*.nupkg")
                .FirstOrDefault();

            if (string.IsNullOrEmpty(packageFilename))
                throw new ApplicationException("No package found matching package ID 'jenkinsnet'!");

            await nugetCore.PushAsync(packageFilename, token);
        }
    }
}
