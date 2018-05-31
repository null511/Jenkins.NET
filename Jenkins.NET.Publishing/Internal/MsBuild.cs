using Photon.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jenkins.NET.Publishing.Internal
{
    internal class MsBuild
    {
        private readonly IDomainContext context;

        public string Exe {get; set;}
        public string Filename {get; set;}
        public string Configuration {get; set;}
        public string Platform {get; set;}
        public bool Parallel {get; set;}


        public MsBuild(IDomainContext context)
        {
            this.context = context;

            Exe = "MsBuild.exe";
        }

        public async Task BuildAsync()
        {
            if (string.IsNullOrEmpty(Filename))
                throw new ArgumentNullException(nameof(Filename));

            var args = new List<string> {
                Filename,
                "/t:Rebuild",
                "/v:m",
            };

            if (!string.IsNullOrEmpty(Configuration))
                args.Add($"/p:Configuration=\"{Configuration}\"");

            if (!string.IsNullOrEmpty(Platform))
                args.Add($"/p:Platform=\"{Platform}\"");

            if (Parallel)
                args.Add("/m");

            await context.RunCommandLineAsync(Exe, args.ToArray());
        }
    }
}
