using Lendmatic.HotUpdatePlugin.Objects;
using Lendmatic.HotUpdatePlugin.Objects.Interface;
using EllieMae.EMLite.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EllieMae.EMLite.RemotingServices;
using Lendmatic.HotUpdatePlugin.Objects.Helpers;
using System.Reflection;
using EllieMae.EMLite.Compiler;
using System.IO;
using System.Security.Policy;
using EllieMae.Encompass.Automation;
using EllieMae.EMLite.ClientServer;

namespace Lendmatic.HotUpdatePlugin.Objects
{
    internal static class PluginLoader
    {

        internal static void LoadHotUpdatePlugins()
        {

            using (ShadowedCache shadowedCache = new ShadowedCache("Plugins"))
            {

                try
                {

                    var plugins = EncompassHelper.RemoteSession.StartupInfo.Plugins;
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

                    foreach (var pluginInfo in plugins)
                    {
                        string assemblyPath = shadowedCache.GetFilePath(pluginInfo.Name);
                        string assmName = Path.GetFileNameWithoutExtension(assemblyPath);

                        // The plugin should already be loaded into the assembly cache
                        var appAssm = assemblies.FirstOrDefault(w => w.FullName.Contains(assmName));
                        if (appAssm != null)
                        {
                            var loadedAssmTypes = appAssm.GetTypes();
                            var loadedTypesWithAttr = loadedAssmTypes.Where(s => s.GetCustomAttributes(true).Any(a => a.GetType().FullName.Contains("HotUpdatePlugin.Objects.Attributes.HotUpdatePluginAttribute")));

                            if (loadedTypesWithAttr.Count() > 0)
                            {
                                RunningPlugin rp = new RunningPlugin();
                                // Load the updated plugin assembly
                                Assembly assembly = RuntimeContext.Current.LoadAssembly(assemblyPath, EncompassHelper.SessionObjects.StartupInfo.RevertPluginChanges);
                                rp.Assembly = assembly;
                                var assmTypes = assembly.GetTypes();
                                var typesWithAttr = assmTypes.Where(s => s.GetCustomAttributes(true).Any(a => a.GetType().FullName.Contains("HotUpdatePlugin.Objects.Attributes.HotUpdatePluginAttribute")));

                                List<object> instances = new List<object>();
                                foreach (Type type in typesWithAttr)
                                {
                                    object pluginInstance = type.InvokeMember("", BindingFlags.CreateInstance, null, null, new object[0]);
                                    instances.Add(pluginInstance);
                                }
                                rp.Instances = instances;
                                RunningPlugins.Plugins.Add(pluginInfo.Name, rp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.HandleError(ex, nameof(PluginLoader), (object)null);
                }

            }

        }

        internal static void UpdatePlugin(PluginInfo pluginInfo)
        {

            using (ShadowedCache shadowedCache = new ShadowedCache("Plugins"))
            {

                Version fileVersion = shadowedCache.GetFileVersion(pluginInfo.Name);
                Version version = pluginInfo.Version;
                // Download the latest server plugin and put into the cache
                if (fileVersion == null || version != fileVersion)
                {
                    shadowedCache.Put(pluginInfo.Name, EncompassHelper.SessionObjects.ConfigurationManager.GetPluginAssembly(pluginInfo.Name));
                }

                // Dispose of the existing hot updateable plugin
                RunningPlugins.Plugins.TryGetValue(pluginInfo.Name, out RunningPlugin runningPlugin);
                if (runningPlugin != null)
                {
                    foreach (var instance in runningPlugin.Instances)
                    {
                        Type runningType = instance.GetType();
                        runningType.InvokeMember("Dispose", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, instance, new object[0]);
                    }
                    runningPlugin.Instances = null;
                    runningPlugin.Assembly = null;
                    RunningPlugins.Plugins.Remove(pluginInfo.Name);
                    GC.Collect();
                }

                RunningPlugin rp = new RunningPlugin();
                // Load the updated plugin assembly
                string assemblyPath = shadowedCache.GetFilePath(pluginInfo.Name);
                Assembly assembly = RuntimeContext.Current.LoadAssembly(assemblyPath, EncompassHelper.SessionObjects.StartupInfo.RevertPluginChanges);
                rp.Assembly = assembly;
                var assmTypes = assembly.GetTypes();
                var typesWithAttr = assmTypes.Where(s => s.GetCustomAttributes(true).Any(a => a.GetType().FullName.Contains("HotUpdatePlugin.Objects.Attributes.HotUpdatePluginAttribute")));

                List<object> instances = new List<object>();
                foreach (Type type in typesWithAttr)
                {
                    object pluginInstance = type.InvokeMember("", BindingFlags.CreateInstance, null, null, new object[0]);
                    instances.Add(pluginInstance);
                }
                rp.Instances = instances;
                RunningPlugins.Plugins.Add(pluginInfo.Name, rp);

            }

        }

    }
}
