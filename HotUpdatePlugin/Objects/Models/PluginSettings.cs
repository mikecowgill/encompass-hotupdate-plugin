﻿using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Lendmatic.HotUpdatePlugin.Objects.Models
{
    public class PluginSettings
    {
        public string PluginName { get; set; }

        public Permission Permissions { get; set; }
        public Dictionary<string, JValue> Settings { get; set; }

        public class Permission
        {
            public bool Everyone { get; set; }
            public List<string> Personas { get; set; }
            public List<string> UserIDs { get; set; }
        }
    }
}