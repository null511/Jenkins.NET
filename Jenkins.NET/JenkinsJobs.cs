using JenkinsNET.Commands;
using JenkinsNET.Exceptions;
using JenkinsNET.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JenkinsNET
{
    public class JenkinsJobs
    {
        private readonly IJenkinsContext context;


        internal JenkinsJobs(IJenkinsContext context)
        {
            this.context = context;
        }

        public JenkinsBuildResult Build(string jobName)
        {
            try {
                var cmd = new JobBuildCommand(context, jobName);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

        public async Task<JenkinsBuildResult> BuildAsync(string jobName)
        {
            try {
                var cmd = new JobBuildCommand(context, jobName);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

        public JenkinsBuildResult BuildWithParameters(string jobName, IDictionary<string, string> jobParameters)
        {
            try {
                var cmd = new JobBuildWithParametersCommand(context, jobName, jobParameters);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

        public async Task<JenkinsBuildResult> BuildWithParametersAsync(string jobName, IDictionary<string, string> jobParameters)
        {
            try {
                var cmd = new JobBuildWithParametersCommand(context, jobName, jobParameters);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobBuildException($"Failed to build Jenkins Job '{jobName}'!", error);
            }
        }

        public JenkinsBuild GetBuild(string jobName, int buildNumber)
        {
            try {
                var cmd = new JobGetBuildCommand(context, jobName, buildNumber);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobGetBuildException($"Failed to retrieve build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        public async Task<JenkinsBuild> GetBuildAsync(string jobName, int buildNumber)
        {
            try {
                var cmd = new JobGetBuildCommand(context, jobName, buildNumber);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobGetBuildException($"Failed to retrieve build #{buildNumber} of Jenkins Job '{jobName}'!", error);
            }
        }

        public JenkinsBuild GetLastBuild(string jobName)
        {
            try {
                var cmd = new JobGetLastBuildCommand(context, jobName);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobGetBuildException($"Failed to retrieve last build of Jenkins Job '{jobName}'!", error);
            }
        }

        public async Task<JenkinsBuild> GetLastBuildAsync(string jobName)
        {
            try {
                var cmd = new JobGetLastBuildCommand(context, jobName);
                await cmd.RunAsync();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsJobGetBuildException($"Failed to retrieve last build of Jenkins Job '{jobName}'!", error);
            }
        }

        public void Delete(string jobName)
        {
            try {
                new JobDeleteCommand(context, jobName).Run();
            }
            catch (Exception error) {
                throw new JenkinsJobDeleteException($"Failed to delete Jenkins Job '{jobName}'!", error);
            }
        }

        public async Task DeleteAsync(string jobName)
        {
            try {
                await new JobDeleteCommand(context, jobName).RunAsync();
            }
            catch (Exception error) {
                throw new JenkinsJobDeleteException($"Failed to delete Jenkins Job '{jobName}'!", error);
            }
        }
    }
}
