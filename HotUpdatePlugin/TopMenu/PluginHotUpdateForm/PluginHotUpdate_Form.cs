using EllieMae.EMLite.Common.UI;
using EllieMae.EMLite.MainUI;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Users;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Collections.Generic;
using Lendmatic.HotUpdatePlugin.Objects.Helpers;
using EllieMae.EMLite.RemotingServices;
using EllieMae.EMLite.ClientServer;
using System.Reflection;
using EllieMae.EMLite.Compiler;
using Lendmatic.HotUpdatePlugin.Objects;
using System.Drawing;
using Lendmatic.HotUpdatePlugin.Objects.Models;

namespace Lendmatic.HotUpdatePlugin.TopMenu.PluginHotUpdateForm
{
    public partial class PluginHotUpdate_Form : Form
    {

        public PluginHotUpdate_Form()
        {
            InitializeComponent();
            setupDataGridView();
            refreshGrid();
        }

        private void refreshGrid()
        {
            grdPlugins.Rows.Clear();
            populateDataGridView();
        }

        private void setupDataGridView()
        {

            grdPlugins.RowHeadersVisible = false;

            grdPlugins.ColumnCount = 3;

            grdPlugins.Columns[0].Name = "Plugin Name";
            grdPlugins.Columns[0].Width = 300;
            grdPlugins.Columns[1].Name = "Client Version";
            grdPlugins.Columns[1].Width = 100;
            grdPlugins.Columns[2].Name = "Server Version";
            grdPlugins.Columns[2].Width = 100;

            grdPlugins.MultiSelect = false;
            grdPlugins.Dock = DockStyle.Fill;

            DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
            updateButton.Name = "update_column";
            updateButton.Text = "Update";
            updateButton.HeaderText = "Update";
            updateButton.UseColumnTextForButtonValue = false;
            if (grdPlugins.Columns["update_column"] == null)
            {
                grdPlugins.Columns.Insert(3, updateButton);
            }
            grdPlugins.CellContentClick -= GrdPlugins_CellContentClick;
            grdPlugins.CellContentClick += GrdPlugins_CellContentClick;
        }

        private void GrdPlugins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grdPlugins = (DataGridView)sender;

            if (grdPlugins.Rows.Count > 0 && grdPlugins.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                using (ShadowedCache shadowedCache = new ShadowedCache("Plugins"))
                {
                    // Get the plugin name
                    string pluginName = (string)grdPlugins.Rows[e.RowIndex].Cells[0].Value;
                    var updatedSsi = EncompassHelper.SessionObjects.Session.GetSessionStartupInfo();
                    var serverPluginInfo = updatedSsi.Plugins.FirstOrDefault(f => f.Name == pluginName);
                    Version fileVersion = shadowedCache.GetFileVersion(serverPluginInfo.Name);

                    if (fileVersion == null || serverPluginInfo.Version != fileVersion)
                    {
                        if (chkUpdateAll.Checked)
                        {
                            EncompassApplication.Session.DataExchange.PostDataToAll(serverPluginInfo);
                        }
                        else
                        {
                            try
                            {
                                PluginLoader.UpdatePlugin(serverPluginInfo);
                                MessageBox.Show($"Successfuly Hot Updated {serverPluginInfo.Name}");
                            }
                            catch (Exception ex)
                            {
                                Logger.HandleError(ex, nameof(PluginHotUpdate_Form), (object)null);
                                MessageBox.Show($"Failed to Hot Update {serverPluginInfo.Name}{Environment.NewLine}Error:{Environment.NewLine}{ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Client Version [{fileVersion}] matches Server Version [{serverPluginInfo.Version}]{Environment.NewLine}{Environment.NewLine}Update will not be run");
                    }
                }

                refreshGrid();
            }
        }

        private void populateDataGridView()
        {
            // List all plugins in the UI and enable the update button for out of date plugins
            var plugins = EncompassHelper.RemoteSession.StartupInfo.Plugins;
            var updatedSsi = EncompassHelper.SessionObjects.Session.GetSessionStartupInfo();

            using (ShadowedCache shadowedCache = new ShadowedCache("Plugins"))
            {
                foreach (var pluginInfo in plugins)
                {
                    // Check if the plugin supports hot updating
                    bool pluginIsHotUpdate = RunningPlugins.Plugins.ContainsKey(pluginInfo.Name);

                    if (pluginIsHotUpdate)
                    {
                        Version fileVersion = shadowedCache.GetFileVersion(pluginInfo.Name);
                        // Find the latest plugin version
                        var serverPluginInfo = updatedSsi.Plugins.FirstOrDefault(f => f.Name == pluginInfo.Name);
                        string[] row = { pluginInfo.Name, fileVersion.ToString(), serverPluginInfo.Version.ToString(), "Update" };

                        grdPlugins.Rows.Add(row);

                        if (fileVersion == null || serverPluginInfo.Version != fileVersion)
                        {
                            grdPlugins.Rows[grdPlugins.Rows.Count-1].Cells[1].Style.BackColor = Color.DarkRed;
                        }

                    }
                    
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshGrid();
        }
    }

}
