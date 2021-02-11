using EllieMae.Encompass.BusinessObjects.Loans;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface ILogEntryChanged
    {
        void LogEntryChanged(object sender, LogEntryEventArgs e);
    }
}
