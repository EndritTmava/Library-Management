﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenaxhimiBibliotekes.BO
{
    public class Language : Audit
    {
        public int LanguageId { get; set; }
        public string _Language { get; set; }
        public bool isActive { get; set; }
    }
}
