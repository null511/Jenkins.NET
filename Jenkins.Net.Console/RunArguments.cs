using System;
using System.Collections.Generic;
using System.Linq;
using JenkinsNET.Console.Internal;
using System.Threading;
using System.Threading.Tasks;
using JenkinsNET.Utilities;
using SysConsole = System.Console;

namespace JenkinsNET.Console
{
    internal class RunArguments : ArgumentsGroup
    {
        public Dictionary<string, string> JobParameters {get;}
        public string JobName {get; private set;}
        public bool ShowHelp {get; private set;}


        public RunArguments()
        {
            Map("-job").ToAction(v => JobName = v);
            Map("-p").ToAction(AddProperty);
            Map("-help", "-?").ToAction(v => ShowHelp = v, false);

            JobParameters = new Dictionary<string, string>();
        }

        public async Task RunAsync(CancellationToken token)
        {
            if (ShowHelp) {
                PrintHelp();
                return;
            }

            SysConsole.ForegroundColor = ConsoleColor.Cyan;
            SysConsole.WriteLine($"Starting Job \"{JobName}\"...");

            var client = new JenkinsClient {
                BaseUrl = "http://localhost:8080/",
                // TODO: Load from local file
            };

            var runner = new JenkinsJobRunner(client) {
                MonitorConsoleOutput = true,
                // TODO: Setup
            };

            runner.StatusChanged += () => {
                SysConsole.ForegroundColor = ConsoleColor.White;
                SysConsole.WriteLine($"[STATUS] {runner.Status}");
            };

            runner.ConsoleOutputChanged += text => {
                SysConsole.ResetColor();
                SysConsole.Write(text);
            };

            if (JobParameters.Any()) {
                await runner.RunWithParametersAsync(JobName, JobParameters, token);
            }
            else {
                await runner.RunAsync(JobName, token);
            }
        }

        private void AddProperty(string property)
        {
            var x = property.IndexOf('=');
            if (x < 0) throw new ApplicationException($"No value specified in property string '{property}'!");
            
            var key = property.Substring(0, x);
            var value = property.Substring(x + 1);
            JobParameters[key] = value;
        }

        private static void PrintHelp()
        {
            SysConsole.ResetColor();
            SysConsole.Write("Arguments: ");
            SysConsole.ForegroundColor = ConsoleColor.DarkCyan;
            SysConsole.WriteLine("run");
            SysConsole.ForegroundColor = ConsoleColor.White;
            SysConsole.WriteLine("  -job <name>");
            SysConsole.WriteLine("  -p:<key>=<value>");
            SysConsole.WriteLine("  -help | -?");
        }
    }
}
