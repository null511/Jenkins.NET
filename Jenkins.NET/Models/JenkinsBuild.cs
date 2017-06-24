using JenkinsNET.Internal;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsNET.Models
{
    public class JenkinsBuild
    {
        private readonly XNode node;

        public IEnumerable<JenkinsAction> Actions => node.WrapGroup("action", n => new JenkinsAction(n));
        public IEnumerable<JenkinsArtifact> Artifacts => node.WrapGroup("artifact", n => new JenkinsArtifact(n));
        public JenkinsPreviousBuild PreviousBuild => node.Wrap("previousBuild", n => new JenkinsPreviousBuild(n));

        public string Class => node?.TryGetValue<string>("@_class");
        public int? Id => node?.TryGetValue<int?>("id");
        public int? Number => node?.TryGetValue<int?>("number");
        public int? QueueId => node?.TryGetValue<int?>("queueId");
        public bool? KeepLog => node?.TryGetValue<bool?>("keepLog");
        public bool? Building => node?.TryGetValue<bool?>("building");
        public string Url => node?.TryGetValue<string>("url");
        public string DisplayName => node?.TryGetValue<string>("displayName");
        public string FullDisplayName => node?.TryGetValue<string>("fullDisplayName");
        public string Result => node?.TryGetValue<string>("result");
        public long? Duration => node?.TryGetValue<long?>("duration");
        public long? EstimatedDuration => node?.TryGetValue<long?>("estimatedDuration");
        public long? TimeStamp => node?.TryGetValue<long?>("timestamp");


        internal JenkinsBuild(XNode node)
        {
            this.node = node;
        }
    }
}
