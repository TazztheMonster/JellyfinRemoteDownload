using MediaBrowser.Model.Plugins;

namespace JellyfinRemoteDownload
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string RemoteJellyfinUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
