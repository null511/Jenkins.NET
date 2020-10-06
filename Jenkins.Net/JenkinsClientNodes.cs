using System;
using System.Collections.Generic;
using JenkinsNET.Exceptions;
using JenkinsNET.Internal.Commands;
using JenkinsNET.Models;

namespace JenkinsNET
{
    public class JenkinsClientNodes
    {
        private readonly IJenkinsContext context;


        internal JenkinsClientNodes(IJenkinsContext context)
        {
            this.context = context;
        }
        
        /// <summary>
        /// Gets the Jenkins Node information
        /// </summary>
        /// <exception cref="JenkinsNetException"></exception>
        public IEnumerable<JenkinsNode> Get()
        {
            try {
                var cmd = new NodesGetCommand(context);
                cmd.Run();
                return cmd.Result;
            }
            catch (Exception error) {
                throw new JenkinsNetException("Failed to retrieve Jenkins description!", error);
            }
        }
        
    }
}