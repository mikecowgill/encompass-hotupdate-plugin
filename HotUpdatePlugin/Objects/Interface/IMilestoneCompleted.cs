using EllieMae.Encompass.BusinessObjects.Loans;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface IMilestoneCompleted
    {
        void MilestoneCompleted(object sender, MilestoneEventArgs e);
    }
}
