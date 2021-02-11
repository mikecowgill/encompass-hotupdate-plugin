using EllieMae.Encompass.BusinessObjects.Loans;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface IBeforeMilestoneCompleted
    {
        void BeforeMilestoneCompleted(object sender, CancelableMilestoneEventArgs e);
    }
}
