using EllieMae.EMLite.ClientServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Lendmatic.HotUpdatePlugin.Objects.Models
{
    [Serializable]
    public class PluginHotUpdateMessage
    {
        public PluginInfo PluginInfo { get; set; }
    }
}
