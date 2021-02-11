using EllieMae.Encompass.BusinessObjects;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface ICommitted
    {
        void Committed(object sender, PersistentObjectEventHandler e);
    }
}
