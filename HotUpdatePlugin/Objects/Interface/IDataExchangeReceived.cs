using EllieMae.Encompass.Client;
using System;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface IDataExchangeReceived
    {
        void DataExchangeReceived(object sender, DataExchangeEventArgs e);
    }
}
