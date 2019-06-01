using System;
using System.Collections.Generic;
using System.Linq;

namespace JenkinsNET.Console.Internal
{
    internal static class ExceptionExtensions
    {
        public static IEnumerable<Exception> UnfoldExceptions(this Exception error)
        {
            var e = error;
            while (e != null) {
                yield return e;
                e = e.InnerException;
            }
        }

        public static string UnfoldMessages(this Exception error)
        {
            var errors = error.UnfoldExceptions().Select(x => x.Message);
            return string.Join(" ", errors);
        }
    }
}
