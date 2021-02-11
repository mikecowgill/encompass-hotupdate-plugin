using Lendmatic.HotUpdatePlugin.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lendmatic.HotUpdatePlugin.TopMenu.PluginHotUpdateForm
{
    public class PluginHotUpdate : MenuItemBase
    {

        public override string Title { get { return "Plugin Hot Update"; } }

        public override bool CanRun()
        {
            return PluginAccess.CheckAccess(nameof(PluginHotUpdate));
        }

        protected override void menuItem_Click(object sender, EventArgs e)
        {
            PluginHotUpdate_Form form = new PluginHotUpdate_Form();
            form.Show();
        }
    }
}
