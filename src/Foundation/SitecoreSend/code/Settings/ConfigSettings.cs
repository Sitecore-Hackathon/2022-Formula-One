using static Sitecore.Configuration.Settings;

namespace Hack2022.Foundation.SitecoreSend.Settings
{
    public struct SendSettings
    {
        public static string MailServerUrl => GetSetting("MailServerUrl");

        public static string MemberListId => GetSetting("MemberListId");

        public static string MooSendApiKey => GetSetting("MooSendApiKey");
    }
}