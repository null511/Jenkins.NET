#if NET_ASYNC
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsNET.Internal
{
    internal static class StreamExtensions
    {
        public static async Task CopyToAsync(this Stream srcStream, Stream destStream, CancellationToken token)
        {
            const int bufferSize = 4096;
            var buffer = new byte[bufferSize];

            while (true) {
                token.ThrowIfCancellationRequested();

                var readSize = await srcStream.ReadAsync(buffer, 0, bufferSize, token);
                if (readSize == 0) break;

                await destStream.WriteAsync(buffer, 0, readSize, token);
            }
        }

        public static async Task<string> ReadToEndAsync(this Stream stream, Encoding encoding, CancellationToken token)
        {
            using (var bufferStream = new MemoryStream()) {
                await stream.CopyToAsync(bufferStream, token);
                return Encoding.UTF8.GetString(bufferStream.ToArray());
            }
        }
    }
}
#endif
