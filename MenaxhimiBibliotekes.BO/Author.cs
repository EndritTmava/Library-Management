﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenaxhimiBibliotekes.BO
{
    public class Author : Audit
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
    }
}
