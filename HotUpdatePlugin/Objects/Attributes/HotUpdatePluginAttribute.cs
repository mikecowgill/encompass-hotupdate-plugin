using System;

namespace Lendmatic.HotUpdatePlugin.Objects.Attributes
{
    /// <summary>
    /// Marks a class as being an Encompass plugin that can be updated while Encompass is running
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HotUpdatePluginAttribute : Attribute
    {
        public HotUpdatePluginAttribute()
        {
        }

    }
}
