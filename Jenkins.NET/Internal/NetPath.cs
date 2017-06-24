using System.Linq;

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
    }
}
