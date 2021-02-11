﻿using System;
using System.Windows.Forms;

namespace Lendmatic.HotUpdatePlugin.Objects.Args
{
    public class FormOpenedArgs: EventArgs
    {
        private Form Form;

        public Form OpenForm { get { return Form; } set { Form = value; } }

        public FormOpenedArgs(Form form) => Form = form;
    }
}
 