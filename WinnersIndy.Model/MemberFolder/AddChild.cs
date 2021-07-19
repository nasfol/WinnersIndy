using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;

namespace WinnersIndy.Model.MemberFolder
{
    public class AddChild
    {
        public int MemberId { get; set; }
        public int ChildrenClassId { get; set; }


        public ICollection<ChildrenClass> ChildrenClasses = new List<ChildrenClass>();
    }
}
