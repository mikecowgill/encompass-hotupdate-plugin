using EllieMae.Encompass.BusinessObjects.Loans;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface ILogEntryAdded
    {
        void LogEntryAdded(object sender, LogEntryEventArgs e);
    }
}
