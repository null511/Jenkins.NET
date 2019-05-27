# Jenkins.NET <img src="https://github.com/null511/Jenkins.NET/raw/master/media/icon.png" height="32" /> <img src="https://travis-ci.org/null511/Jenkins.NET.svg?branch=master" />

C# .NET wrapper for Jenkins HTTP/REST API. Supports .NET Framework 4.0; 4.5 and .NET Standard 2.0, as well as the asynchronous programming model. Package available on [NuGet](https://www.nuget.org/packages/jenkinsnet).

This project is now considered stable, and not actively being developed. However, if you find a bug or feature missing please file an Issue here on GitHub and I will adress it as soon as possible. Thank you for your interest!

Visit the [Wiki](https://github.com/null511/Jenkins.NET/wiki) for Code Examples.

-----

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
