﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenaxhimiBibliotekes.BO
{
   public class MonthBorrowStatistic
    {
        public int BorrowingsCount { get; set; }
        public string Month { get; set; }


        public void SetMonths(int i)
        {
            switch (i)
            {
                case 1:
                    Month = "January";
                    break;
                case 2:
                    Month = "Febuary";
                    break;
                case 3:
                    Month = "March";
                    break;
                case 4:
                    Month = "April";
                    break;
                case 5:
                    Month = "May";
                    break;
                case 6:
                    Month = "June";
                    break;
                case 7:
                    Month = "July";
                    break;
                case 8:
                    Month = "August";
                    break;
                case 9:
                    Month = "September";
                    break;
                case 10:
                    Month = "October";
                    break;
                case 11:
                    Month = "November";
                    break;
                case 12:
                    Month = "December";

                    break;

            }
        }
    }
}
