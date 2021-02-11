using EllieMae.EMLite.ClientServer;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.Client;
using Lendmatic.HotUpdatePlugin.Objects;
using Lendmatic.HotUpdatePlugin.Objects.Helpers;
using Lendmatic.HotUpdatePlugin.Objects.Interface;
using Lendmatic.HotUpdatePlugin.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lendmatic.HotUpdatePlugin.UpdateNotification
{
    public class PluginUpdateDataExchange : Plugin, IDataExchangeReceived
    {

        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(PluginUpdateDataExchange));
        }

        public override void DataExchangeReceived(object sender, EllieMae.Encompass.Client.DataExchangeEventArgs e)
        {
            if (e.Data is PluginInfo)
            {
                PluginInfo pluginInfo = e.Data as PluginInfo;
                
                if (EncompassApplication.Screens.InvokeRequired)
                {
                    EncompassApplication.Screens.Invoke(new DataExchangeEventHandler(DataExchangeReceived), new object[] { sender, e });
                }
                else
                {
                    try
                    {
                        PluginLoader.UpdatePlugin(pluginInfo);
                        // MessageBox.Show($"Successfuly Hot Updated {pluginInfo.Name}");
                    }
                    catch (Exception ex)
                    {
                        Logger.HandleError(ex, nameof(PluginUpdateDataExchange), (object)null);
                        // MessageBox.Show($"Failed to Hot Update {pluginInfo.Name}{Environment.NewLine}Error:{Environment.NewLine}{ex.Message}");
                    }             
                }
                
            }
            
        }

    }
}
