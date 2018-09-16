using Photon.Framework.Server;
using Photon.NuGet.CorePlugin;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

            var packageFilename = Directory
                .GetFiles(Context.ContentDirectory, "jenkinsnet.*.nupkg")
                .FirstOrDefault();

            if (string.IsNullOrEmpty(packageFilename))
                throw new ApplicationException("No package found matching package ID 'jenkinsnet'!");

            await nugetCore.PushAsync(packageFilename, token);
        }
    }
}
