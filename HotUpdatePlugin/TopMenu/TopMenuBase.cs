using Lendmatic.HotUpdatePlugin.Objects;
using Lendmatic.HotUpdatePlugin.Objects.Interface;
using EllieMae.EMLite.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lendmatic.HotUpdatePlugin.Properties;

namespace Lendmatic.HotUpdatePlugin.TopMenu
{
    public class TopMenuBase : Plugin, ILogin
    {
        List<ToolStripItem> active;
        public override bool Authorized()
        {
            return PluginAccess.CheckAccess(nameof(TopMenuBase));
        }

        public override void Login(object sender, EventArgs e)
        {
            createLendmaticMenu();
        }

        private void createLendmaticMenu()
        {
            GradientMenuStrip menu = (GradientMenuStrip)FormWrapper.Find("mainMenu");
            ToolStripMenuItem item = menu.Items[0] as ToolStripMenuItem;
            var items = item.DropDownItems.Cast<ToolStripItem>().ToList();

            var lendmaticMenu = items.FirstOrDefault(f => f.Text == "Lendmatic");
            if (lendmaticMenu == null)
            {
                lendmaticMenu = new ToolStripMenuItem("Lendmatic");
                lendmaticMenu.Image = new Bitmap(Resource.lendmatic_icon);

                var settingsIndex = item.DropDownItems.Cast<ToolStripItem>().ToList().FindIndex(f => f.Name == "tsMenuItemCompanySettings");
                if (settingsIndex != -1)
                {
                    item.DropDownItems.Insert(settingsIndex, lendmaticMenu);
                }
                else
                {
                    item.DropDownItems.Insert(0, lendmaticMenu);
                }
            }

            var m = lendmaticMenu as ToolStripMenuItem;
            m.DropDownItems.AddRange(GetDropDownItems());
        }

        private ToolStripItem[] GetDropDownItems()
        {
            active = new List<ToolStripItem>();
            foreach (Type type in ((IEnumerable<Type>)this.GetType().Assembly.GetTypes()).Where<Type>((Func<Type, bool>)(type => type.IsSubclassOf(typeof(MenuItemBase)))).ToList<Type>())
            {
                try
                {
                    MenuItemBase menuItemBaseClass = Activator.CreateInstance(type) as MenuItemBase;
                    if (menuItemBaseClass != null && menuItemBaseClass.CanRun() && this.active.FirstOrDefault<ToolStripItem>(x => x.GetType() == menuItemBaseClass.GetType()) == null)
                    {
                        this.active.Add(menuItemBaseClass.CreateToolStripMenu((Image)null, menuItemBaseClass.Title));
                    }
                }
                catch (Exception ex)
                {
                    Logger.HandleError(ex, nameof(TopMenuBase), (object)null);
                }
            }

            return active.ToArray();
        }
    }
}
