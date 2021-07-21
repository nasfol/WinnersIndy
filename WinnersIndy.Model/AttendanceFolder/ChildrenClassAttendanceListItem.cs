using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.AttendanceFolder
{
    public class ChildrenClassAttendanceListItem
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool InChurch { get; set; }
        
        public DateTime AttendanceDate { get; set; }
    }

}
