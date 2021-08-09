using JenkinsNET.Exceptions;
using JenkinsNET.Internal.Commands;
using JenkinsNET.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsNET
{
    /// <summary>
    /// A collection of methods used for interacting with Jenkins Views API.
    /// </summary>
    /// <remarks>
    /// Used internally by <seealso cref="JenkinsClient"/>
    /// </remarks>
    public sealed class JenkinsClientViews
    {
        private readonly IJenkinsContext _context;

        internal JenkinsClientViews(IJenkinsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new View in Jenkins.
        /// </summary>
        /// <param name="viewName">The name of the View to create.</param>
        /// <param name="view">The description of the Job to create.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public void Create(string viewName, JenkinsProject view)
        {
            try
            {
                new ViewCreateCommand(_context, viewName, view).Run();
            }
            catch (Exception error)
            {
                throw new JenkinsNetException($"Failed to create Jenkins View '{viewName}'!", error);
            }
        }

#if NET_ASYNC
        /// <summary>
        /// Creates a new View in Jenkins asynchronously.
        /// </summary>
        /// <param name="viewName">The name of the View to create.</param>
        /// <param name="view">The description of the View to create.</param>
        /// <param name="token">An optional token for aborting the request.</param>
        /// <exception cref="JenkinsNetException"></exception>
        public async Task CreateAsync(string viewName, JenkinsProject view, CancellationToken token = default)
        {
            try
            {
                await new ViewCreateCommand(_context, viewName, view).RunAsync(token);
            }
            catch (Exception error)
            {
                throw new JenkinsNetException($"Failed to create Jenkins Job '{viewName}'!", error);
            }
        }
#endif
    }
}
