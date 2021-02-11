using Lendmatic.HotUpdatePlugin.Objects;
using EllieMae.Encompass.ComponentModel;

namespace Lendmatic.HotUpdatePlugin
{
    [Plugin]
    public class PluginEntry
    {
        public PluginEntry()
        {
            PluginLoader.LoadHotUpdatePlugins();
            Plugins.Start();
        }
    }
}
