using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.AttendanceFolder
{
   public class AttendanceListItem
    {
        public DateTimeOffset AttendanceDate { get; set; }


        public IEnumerable<ChildrenClassAttendanceListItem> ListChildrenClassAttendanceListItem { get; set; }
    }
}
