using JenkinsNET.Commands;
using JenkinsNET.Exceptions;
using JenkinsNET.Models;
using System;
using System.Threading.Tasks;

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with Jenkins Builds.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public class JenkinsClientBuilds
    {
        private readonly IJenkinsContext context;


        internal JenkinsClientBuilds(IJenkinsContext context)
        {
            this.context = context;
        }

        public JenkinsBuild Get(string jobName, string buildNumber = "lastSuccessfulBuild")
        {
            try {
                var cmd = new BuildGetCommand(context, jobName, buildNumber);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobGetBuildException($"Failed to retrieve build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        public async Task<JenkinsBuild> GetAsync(string jobName, string buildNumber = "lastSuccessfulBuild")
        {
            try {
                var cmd = new BuildGetCommand(context, jobName, buildNumber);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobGetBuildException($"Failed to retrieve build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }
    }
}
