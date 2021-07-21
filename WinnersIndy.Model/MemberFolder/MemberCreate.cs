using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Address { get; set; }
        [Required, Range(1, 3, ErrorMessage = "Select form the List")]
        public Status MaritalStatus { get; set; }
        [Required, Range(1, 9, ErrorMessage = "Select form the List")]
        public UnitService ServiceUnit { get; set; }
        [Required, Range(1, 2, ErrorMessage = "Select form the List")]
        public Sex Gender { get; set; }
    }
}
