## Jenkins.NET

C# .NET wrapper for Jenkins HTTP/REST API. Supports asynchronous programming model. Package available on [NuGet](https://www.nuget.org/packages/jenkinsnet).

Visit the [Wiki](https://github.com/null511/Jenkins.NET/wiki) for Code Examples.

-----

# API Summary

## JenkinsClient
- **Get** - Returns the description of the Jenkins node.

## JenkinsClient.Jobs
- **Build** - Queues a Job to be built.
- **BuildWithParameters** - Queues a Job to be built with parameters.
- **Get** - Returns the description of a Job.
- **GetConfiguration** - Returns the configuration of the Job.
- **UpdateConfiguration** - Updates the configuration of an existing Job.
- **Create** - Creates a new Job using the provided Job configuration.
- **Delete** - Deletes an existing Job.

## JenkinsClient.Builds
- **Get** - Returns a Build description.
- **GetConsoleOutput** - Returns the console output of a Build. _May be truncated_.
- **GetProgressiveText** - Returns the progressive text output of a Build, starting from the specified index.
- **GetProgressiveHtml** - Returns the progressive HTML output of a Build, starting from the specified index.

## JenkinsClient.Queue
- **GetItem** - Returns a Queue item representing a pending Build.

## JenkinsClient.Artifacts
- **Get** - Returns an Artifact from a specified Build.
