using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Services
{
    public class MemberServices
    {
        public class MemberServices
        {
            private readonly Guid _userId;
            public MemberServices(Guid userid)
            {
                _userId = userid;
            }

            public bool CreateMember(MemberCreate model)
            {
                byte[] bytes = null;
                if (model.File != null)
                {
                    Stream Fs = model.File.InputStream;
                    BinaryReader Br = new BinaryReader(Fs);
                    bytes = Br.ReadBytes((Int32)Fs.Length);
                }
                var entity =
                    new Member()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        EmailAddress = model.EmailAddress,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        FileContent = bytes,
                        OwnerId = _userId,
                        Gender = model.Gender,
                        ServiceUnit = model.ServiceUnit,
                        MaritalStatus = model.MaritalStatus
                    };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Members.Add(entity);
                    return ctx.SaveChanges() == 1;
                }

            }

            public IEnumerable<MemberListItem> GetMembers()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var members =
                                ctx
                                .Members
                                .Select(e => new MemberListItem
                                {

                                    MemberId = e.MemberId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    EmailAddress = e.EmailAddress,
                                    PhoneNumber = e.PhoneNumber,
                                    DateOfBirth = e.DateOfBirth,


                                }).ToArray();

                    return members;
                }
            }
            //================ Get Individual Member details ===============//
            public MemberDetails GetMemberById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var member =
                            ctx
                            .Members
                            .Single(e => e.MemberId == id);
                    return new MemberDetails()
                    {
                        MemberId = member.MemberId,
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        PhoneNumber = member.PhoneNumber,
                        DateOfBirth = member.DateOfBirth,
                        Address = member.Address,
                        EmailAddress = member.EmailAddress,
                        FileContent = member.FileContent
                    };
                }
            }
            public bool UpdateMember(MemberEdit model)
            {
                byte[] bytes = null;
                if (model.File != null)
                {
                    Stream Fs = model.File.InputStream;
                    BinaryReader Br = new BinaryReader(Fs);
                    bytes = Br.ReadBytes((Int32)Fs.Length);
                }

                using (var ctx = new ApplicationDbContext())
                {
                    var member =
                        ctx
                        .Members
                        .Find(model.MemberId);

                    member.FirstName = model.FirstName;
                    member.LastName = model.LastName;
                    member.PhoneNumber = model.PhoneNumber;
                    member.EmailAddress = model.EmailAddress;
                    member.DateOfBirth = model.DateOfBirth;
                    member.FileContent = bytes;
                    member.ModifiedUtc = DateTimeOffset.Now;
                    member.Address = model.Address;

                    return ctx.SaveChanges() == 1;
                }
            }
            //=============Delete A Member=======================================================//
            public bool DeleteMember(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var member = ctx
                        .Members
                        .SingleOrDefault(e => e.OwnerId == _userId && e.MemberId == id);
                    ctx.Members.Remove(member);
                    return ctx.SaveChanges() == 1;
                }
            }
            //==============List Of Members where their Age is less than 18 as Children===========//
            public List<Children> GetChildren()
            {
                List<Children> ListOfChildren = new List<Children>();
                using (var ctx = new ApplicationDbContext())
                {
                    var members = ctx.Members
                                      .Select(e => new Children()
                                      {
                                          MemberId = e.MemberId,
                                          FirstName = e.FirstName,
                                          LastName = e.LastName,
                                          DateOfBirth = e.DateOfBirth,
                                          Age = DateTime.Now.Year - e.DateOfBirth.Year

                                      }).ToList();


                    foreach (var member in members)
                    {
                        if (member.Age <= 18)
                        {
                            ListOfChildren.Add(member);
                        }
                    }


                }
                return ListOfChildren;

            }
            //================Add A child to Children Class==============//

            public bool AddChildtoclass(AddChild model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var child = ctx
                        .Members
                        .Find(model.MemberId);
                    var childrenclass = ctx
                        .ChildrenClasses
                        .Find(model.ChildrenClassId);
                    childrenclass.Children.Add(child);
                    return ctx.SaveChanges() == 1;
                }
            }
            //==================Get List of Children Classes============//

            public List<ChildrenClass> GetDropdown()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var nvm =
                        ctx
                            .ChildrenClasses
                            .ToList();
                    return nvm;
                }
            }


            //=============List of Members that are Teachers===========//
            public List<MemberListItem> GetTeachers()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var teachers = ctx
                                        .Members
                                        .Where(e => e.ServiceUnit == UnitService.Teacher)
                                        .Select(e => new MemberListItem
                                        {
                                            MemberId = e.MemberId,
                                            FirstName = e.FirstName,
                                            LastName = e.LastName,
                                            EmailAddress = e.EmailAddress,
                                            PhoneNumber = e.PhoneNumber
                                        }).ToList();

                    return teachers;

                }
            }

            public bool AddTeachertoclass(AddChild model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var child = ctx
                        .Members
                        .Find(model.MemberId);
                    var childrenclass = ctx
                        .ChildrenClasses
                        .Find(model.ChildrenClassId);
                    childrenclass.Children.Add(child);
                    return ctx.SaveChanges() == 1;
                }
            }

            public ICollection<Member> GetFamilyMember(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var member = ctx
                                    .Members
                                    .SingleOrDefault(e => e.MemberId == id);

                    var family = ctx
                                            .Families
                                            .SingleOrDefault(e => e.FamilyId == member.FamilyId);
                    if (family is null)
                    {
                        return null;
                    }


                    return family.FamilyMember;
                }
            }
        }

    }

}
