using System.Net.NetworkInformation;
using System.Text;

namespace HeartBeat.Logic.Helpers
{
    /// <summary>
    /// Wraps up simple ICMP Ping Command with a single 
    /// using the System.Net.NetworkInformation Class
    /// </summary>
    public static class Pinger
    {
        /// <summary>
        /// 32 Byte Payload
        /// </summary>
        private const string Data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        /// <summary>
        /// Pings the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        ///   <c>true</c> if the ping succeeds otherwise, <c>false</c>.
        ///</returns>
        public static bool PingServer(string server, int timeout)
        {
            try
            {
                var pingSender = new Ping();
                var buffer = Encoding.ASCII.GetBytes(Data);
                var reply = pingSender.Send(server, timeout, buffer,  new PingOptions {DontFragment = true});
                return reply != null && reply.Status == IPStatus.Success;
            }
            catch
            {
                //var message = string.Format("Exception inside Pinger {0} @ {1}ms. {2}", server, timeout, ex);
                //System.Diagnostics.Debug.WriteLine(message);

                return false;
            }
        }
    }
}
