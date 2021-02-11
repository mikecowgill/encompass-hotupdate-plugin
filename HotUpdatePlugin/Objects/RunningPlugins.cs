using EllieMae.EMLite.Compiler;
using Lendmatic.HotUpdatePlugin.Objects.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lendmatic.HotUpdatePlugin.Objects
{
    public static class RunningPlugins
    {
        public static Dictionary<string, RunningPlugin> Plugins = new Dictionary<string, RunningPlugin>();
    }

    public class RunningPlugin
    {
        public Assembly Assembly { get; set; }
        public List<object> Instances { get; set; }
    }
}
