using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

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

                var eKey = HttpUtility.UrlEncode(arg.Key);
                var eValue = string.Empty;

                if (arg.Value != null)
                    eValue = HttpUtility.UrlEncode(arg.Value.ToString());

                builder.Append(eKey);
                builder.Append("=");
                builder.Append(eValue);
            }

            return builder.ToString();
        }

        private static IEnumerable<KeyValuePair<string, object>> GetQueryArguments(object arguments)
        {
            if (arguments is IDictionary<string, object> dictionary) return dictionary;

            return arguments.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => {
                #if NET40
                    var value = x.GetValue(arguments, null);
                #else
                    var value = x.GetValue(arguments);
                #endif

                    return new KeyValuePair<string, object>(x.Name, value);
                });
        }
    }
}
