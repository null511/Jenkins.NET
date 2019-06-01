using System;
using JenkinsNET.Console.Internal;
using System.Threading;
using System.Threading.Tasks;
using SysConsole = System.Console;

namespace JenkinsNET.Console
{
    internal class Arguments : ArgumentsGroup
    {
        public RunArguments RunGroup {get;}
        public Actions Action {get; private set;}


        public Arguments()
        {
            RunGroup = new RunArguments();

            Map("run").ToGroup(RunGroup, () => Action = Actions.Run);
            Map("-help", "-?").ToAction(v => Action = Actions.Help);
        }

        public Task RunAsync(CancellationToken token = default)
        {
            switch (Action) {
                case Actions.Run:
                    return RunGroup.RunAsync(token);
                default:
                    PrintHelp();
                    break;
            }

            return Task.CompletedTask;
        }

        private static void PrintHelp()
        {
            SysConsole.ResetColor();
            SysConsole.WriteLine("Arguments:");
            SysConsole.ForegroundColor = ConsoleColor.White;
            SysConsole.WriteLine("   run ...");
            SysConsole.WriteLine("  -help | -?");
        }

        public enum Actions
        {
            Undefined,
            Run,
            Help,
        }
    }
}
