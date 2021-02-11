﻿using Lendmatic.HotUpdatePlugin.Objects.Interface;
using System;

namespace Lendmatic.HotUpdatePlugin.Objects
{
    public static class Plugins
    {
        public static void Start()
        {
            InterfaceHelper i = new InterfaceHelper();
            foreach(Type type in i.GetAll(typeof(Plugin)))
                (Activator.CreateInstance(type) as Plugin).Run();
        }
    }
}
