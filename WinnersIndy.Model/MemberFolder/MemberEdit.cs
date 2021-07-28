using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinnersIndy.Model.MemberFolder
{
    public class MemberEdit
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public HttpPostedFileBase File { get; set; }
        public byte[] FileContent { get; set; }
        public string Address { get; set; }
    }
}
