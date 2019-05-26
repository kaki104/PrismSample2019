using System.Runtime.InteropServices;

namespace PrismSample2019.Core.Helpers
{
    /// <summary>
    /// UserAgent 강제 설정
    /// setting a custom user-agent in the uwp webview control
    /// </summary>
    public class UserAgentHelper
    {
        private const int URLMON_OPTION_USERAGENT = 0x10000001;

        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength,
            int dwReserved);

        /// <summary>
        /// SetUserAgent
        /// </summary>
        public static void SetDefaultUserAgent(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return;
            }

            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, userAgent, userAgent.Length, 0);
        }
    }
}
