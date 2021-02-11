using System.Drawing;
using System.Windows.Forms;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface IMenuItemBase
    {
        ToolStripItem CreateToolStripMenu(Image image, string Name);
    }
}
