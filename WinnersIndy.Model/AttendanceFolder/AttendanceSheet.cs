﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.AttendanceFolder
{
    public class AttendanceSheet
    {
        public int MemberId { get; set; }

        public bool Inchurch { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}