using Lendmatic.HotUpdatePlugin.Objects.Helpers;
using EllieMae.Encompass.Client;
using System;

namespace Lendmatic.HotUpdatePlugin.Objects
{
    public static class Logger
    {
        public static void HandleError(Exception Ex, string Name, object data = null)
        {
            try
            {
                if (string.IsNullOrEmpty(Name))
                    return;

                ApplicationLog.WriteError(nameof(HotUpdatePlugin), $"{Name}{Environment.NewLine}{Ex.ToString()}");
            }
            catch(Exception ex)
            {
                Logger.Fatal(ex.ToString());
            }
        }

        private static void Fatal(string Text, object data = null)
        {
            ApplicationLog.WriteError(EncompassHelper.LoanNumber(), "Fatal");
        }
    }
}
