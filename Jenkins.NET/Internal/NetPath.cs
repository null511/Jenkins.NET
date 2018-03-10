using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JenkinsNET.Internal
{
    internal static class NetPath
    {
        public static string Combine(params string[] paths)
        {
            return paths.Aggregate((a, b) => {
                if (string.IsNullOrEmpty(b)) return a;
                if (string.IsNullOrEmpty(a)) return b;
                return string.Concat(a.TrimEnd('/'), '/', b.TrimStart('/'));
            });
        }

        public static string Query(object arguments)
        {
            var args = GetQueryArguments(arguments);

            var builder = new StringBuilder();

            var i = 0;
            foreach (var arg in args) {
                builder.Append(i > 0 ? "&" : "?");
                i++;

                builder.Append(arg.Key);
                builder.Append("=");
                builder.Append(arg.Value);
            }

            return builder.ToString();
        }

        private static IEnumerable<KeyValuePair<string, object>> GetQueryArguments(object arguments)
        {
            if (arguments is IDictionary<string, object> dictionary)
                return dictionary;

            return arguments.GetType()
                .GetProperties(System.Reflection.BindingFlags.Public)
                .Select(x => new KeyValuePair<string, object>(x.Name, x.GetValue(arguments)));
        }
    }
}
