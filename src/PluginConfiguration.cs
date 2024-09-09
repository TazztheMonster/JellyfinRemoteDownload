using MediaBrowser.Model.Plugins;

namespace JellyfinRemoteDownloadPlugin
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string RemoteJellyfinUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
