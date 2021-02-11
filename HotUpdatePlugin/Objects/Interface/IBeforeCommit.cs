using EllieMae.Encompass.BusinessObjects.Loans;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface IBeforeCommit
    {
        void BeforeCommit(object sender, CancelableEventArgs e);
    }
}
