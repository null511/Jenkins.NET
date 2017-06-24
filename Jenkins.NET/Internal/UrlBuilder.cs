using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JenkinsNET.Internal
{
    internal class UrlBuilder
    {
        public string Url {get; set;}
        public Dictionary<string, string> Query {get; set;}


        public UrlBuilder()
        {
            Query = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder(Url);
            if (Query.Any()) AppendQuery(builder);
            return builder.ToString();
        }

        private void AppendQuery(StringBuilder builder)
        {
            var isFirst = true;
            foreach (var pair in Query) {
                if (isFirst) {
                    builder.Append('?');
                    isFirst = false;
                }
                else {
                    builder.Append('&');
                }

                if (string.IsNullOrEmpty(pair.Key))
                    throw new ApplicationException("Parameter name cannot be null or empty!");

                var encodedName = HttpUtility.UrlEncode(pair.Key);
                builder.Append(encodedName);
                builder.Append('=');

                if (!string.IsNullOrEmpty(pair.Value)) {
                    var encodedValue = HttpUtility.UrlEncode(pair.Value);
                    builder.Append(encodedValue);
                }
            }
        }
    }
}
