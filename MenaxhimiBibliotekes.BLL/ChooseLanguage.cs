﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenaxhimiBibliotekes.BLL
{
    public class ChooseLanguage<T,U> : Form
    {
        public static void ChangeLanguage(string langCode, Form form)
        {
            CultureInfo c1 = new CultureInfo(langCode);
            Thread.CurrentThread.CurrentCulture = c1;
            Thread.CurrentThread.CurrentUICulture = c1;

            foreach (Control control in form.Controls)
            {
                ComponentResourceManager resource = new ComponentResourceManager(typeof(T));
                resource.ApplyResources(control, control.Name, c1);
                if (control.GetType() == typeof(TabControl))
                {
                    foreach (Control co in ((TabControl)control).Controls)
                    {

                        string text = resource.GetString(co.Name + ".Text", c1);
                        if (text != null)
                        {
                            co.Text = text;
                        }
                    }
                }



            }








        }

    }




    
}
