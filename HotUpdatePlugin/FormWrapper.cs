﻿using LendmaticEncompass.Objects;
using LendmaticEncompass.Objects.Args;
using EllieMae.EMLite.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LendmaticEncompass.Non_Native_Modifications
{
    public delegate void FormOpenedHandler(object sender, FormOpenedArgs e);
    public static class FormWrapper
    {
        public static event FormOpenedHandler FormOpened;

        public static Form EncompassForm => Application.OpenForms.Cast<Form>().FirstOrDefault();

        public static TabControl TabControl => EncompassForm.Controls.Find("tabControl", true)[0] as TabControl;

        public static HashSet<Form> OpenForms;

        public static Control Find(string ControlID) => EncompassForm.Controls.Find(ControlID, true)?.FirstOrDefault() ?? (Control)null;
        static FormWrapper()
        {
            OpenForms = new HashSet<Form>();
            EncompassForm.Deactivate += EncompassForm_Deactivate;
            Timer t = new Timer();
            t.Interval = 1000;
            t.Enabled = true;
            t.Tick += T_Tick;
        }

        private static void T_Tick(object sender, EventArgs e)
        {
            RefreshFormList();
        }

        private static void EncompassForm_Deactivate(object sender, EventArgs e)
        {
            RefreshFormList();
        }

        private static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form form = sender as Form;
            if (form == null)
                return;

            if (OpenForms.Contains(form))
                OpenForms.Remove(form);

            form.FormClosing -= Form_FormClosing;
        }

        public static GridView GetPipeline()
        {
            Control[] controlArray = EncompassForm?.Controls.Find("gvLoans", true);
            if (controlArray == null || ((IEnumerable<Control>)controlArray).Count<Control>().Equals(0))
                return null;

            return controlArray[0] as GridView;
        }

        private static void RefreshFormList()
        {
            if (Application.OpenForms == null)
                return;

            foreach(Form form in Application.OpenForms)
            {
                try
                {
                    if (form != null && !form.IsDisposed && !OpenForms.Contains(form))
                    {
                        form.FormClosing += Form_FormClosing;
                        OpenForms.Add(form);
                        FormOpenedArgs e = new FormOpenedArgs(form);
                        FormOpened?.Invoke(null, e);
                    }
                }
                catch(Exception ex)
                {
                    Logger.HandleError(ex, nameof(FormWrapper));
                }
            }
        }
    }
}
