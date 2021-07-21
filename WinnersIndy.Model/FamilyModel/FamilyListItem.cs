using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;

namespace WinnersIndy.Model.FamilyModel
{
    public class FamilyListItem
    {
        public int FamilyId { get; set; }
        public DateTime? AniversaryDate { get; set; }
        public string FamilyName { get; set; }

        public virtual ICollection<Member> Parent { get; set; }
    }
}
