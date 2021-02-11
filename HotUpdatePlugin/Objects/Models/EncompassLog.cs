using Lendmatic.HotUpdatePlugin.Objects.Enums;
using System;

namespace Lendmatic.HotUpdatePlugin.Objects.Models
{
    public class EncompassLog
    {
        public DateTime? TimeStamp { get; set; }
        public EncompassLogType Type { get; set; }
        public string Message { get; set; }
    }
}
