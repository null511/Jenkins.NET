using System;
using SysConsole = System.Console;

namespace Jenkins.Net.Console
{
    internal static class Program
    {
    #if !NET_ASYNC
        public static int Main(string[] args)
        {
            SysConsole.ForegroundColor = ConsoleColor.White;
            SysConsole.WriteLine("Jenkins.NET Console");

            try {
                var arguments = new Arguments();
                arguments.Parse(args);

                // TODO: ...

                return 0;
            }
            catch (Exception error) {
                SysConsole.ForegroundColor = ConsoleColor.Red;
                SysConsole.WriteLine("[ERROR] ");
                SysConsole.ForegroundColor = ConsoleColor.DarkRed;
                SysConsole.WriteLine(error.Message);
                return 1;
            }
            finally {
                SysConsole.ResetColor();
            }
        }
    #else
        public static async Task<int> Main(string[] args)
        {
            SysConsole.ForegroundColor = ConsoleColor.White;
            SysConsole.WriteLine("Jenkins.NET Console");

            try {
                var arguments = new Arguments();
                arguments.Parse(args);

                // TODO: ...

                return 0;
            }
            catch (Exception error) {
                SysConsole.ForegroundColor = ConsoleColor.Red;
                SysConsole.WriteLine("[ERROR] ");
                SysConsole.ForegroundColor = ConsoleColor.DarkRed;
                SysConsole.WriteLine(error.Message);
                return 1;
            }
            finally {
                SysConsole.ResetColor();
            }
        }
    #endif
    }
}
