using JenkinsNET.Console.Internal;
using System;
using System.Threading.Tasks;
using SysConsole = System.Console;

namespace JenkinsNET.Console
{
    internal static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            SysConsole.ForegroundColor = ConsoleColor.White;
            SysConsole.WriteLine("Jenkins.NET Console");
            SysConsole.WriteLine();

            try {
                var arguments = new Arguments();
                arguments.Parse(args);

                await arguments.RunAsync();

                return 0;
            }
            catch (Exception error) {
                SysConsole.ForegroundColor = ConsoleColor.Red;
                SysConsole.WriteLine("[ERROR] ");
                SysConsole.ForegroundColor = ConsoleColor.DarkRed;
                SysConsole.WriteLine(error.UnfoldMessages());

                return 1;
            }
            finally {
                SysConsole.ResetColor();
            }
        }
    }
}
