using System.Collections.Generic;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface IFactory
    {
        List<ITask> GetTriggers();
    }
}
