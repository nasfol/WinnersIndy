using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WinnersIndy.Data;

namespace WinnersIndy.Model.MemberFolder
{
    public class MemberCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Address { get; set; }
        public Status MaritalStatus { get; set; }
        public UnitService ServiceUnit { get; set; }
        public Sex Gender { get; set; }
    }
}
