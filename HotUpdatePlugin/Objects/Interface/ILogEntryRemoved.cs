using EllieMae.Encompass.BusinessObjects.Loans;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface ILogEntryRemoved
    {
        void LogEntryRemoved(object sender, LogEntryEventArgs e);
    }
}
