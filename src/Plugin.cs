using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using System;

namespace JellyfinRemoteDownload
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public override string Name => "Remote Jellyfin Download";
        public override string Description => "Download videos from other Jellyfin instances";

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
        }

        // Add a link to the web UI for the plugin
        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "remotedownload",
                    EmbeddedResourcePath = GetType().Namespace + ".Web.remote.html"
                }
            };
        }
    }
}
