using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
   
    
        public enum Sex
        {
            Male = 1,
            Female
        }

        public enum Status
        {
            Single = 1,
            Married,
            Others
        }
        public enum UnitService
        {
            Teacher = 1,
            Usher,
            Minister,
            Hospitality,
            Choir,
            Technical,
            Sanctuary,
            None,
            Child
        }

        public class Member
        {
            public int MemberId { get; set; }
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Required]
            public DateTime DateOfBirth { get; set; }
            [Required]
            public string Address { get; set; }
            [Required, Range(1,2,ErrorMessage ="Select form the List")]
            public Sex Gender { get; set; }

            [ForeignKey(nameof(Family))]
            public int? FamilyId { get; set; }
            public virtual Family Family { get; set; }

            public string PhoneNumber { get; set; }
            public string EmailAddress { get; set; }
            public string FileName { get; set; }
            public byte[] FileContent { get; set; }
            [Required, Range(1, 3, ErrorMessage = "Select form the List")]
            public Status MaritalStatus { get; set; }
            [Required, Range(1, 9, ErrorMessage = "Select form the List")]
            public UnitService ServiceUnit { get; set; }
            public Guid OwnerId { get; set; }
            public DateTimeOffset CreatedUtc { get; set; }
            public DateTimeOffset ModifiedUtc { get; set; }
            public int Age
            {
                get
                {
                    int age = DateTime.Now.Year - DateOfBirth.Year;
                    return age;
                }
            }
            [ForeignKey(nameof(ChildrenClass))]
            public int? ChildrenClassId { get; set; }
            public virtual ChildrenClass ChildrenClass { get; set; }

           



        }
    
}
