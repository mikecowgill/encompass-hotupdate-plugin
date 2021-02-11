using Newtonsoft.Json;

namespace Lendmatic.HotUpdatePlugin.Objects.Models
{
    public class CDO
    {
        [JsonProperty("PluginSettings")]
        public SettingsCDO PluginSettings { get; set; }
    }
}
