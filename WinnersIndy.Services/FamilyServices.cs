using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model.FamilyModel;

namespace WinnersIndy.Services
{
    public class FamilyServices
    {
        private readonly Guid _userId;
        public FamilyServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFamily(FamilyCreate model)
        {

            var family = new Family()
            {
                FamilyName = model.FamilyName,
                AniversaryDate = model.AniversaryDate,
                OwnerId = _userId
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Families.Add(family);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<FamilyListItem> GetFamilies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var families =
                                ctx
                                .Families
                                .Select(e => new FamilyListItem()
                                {
                                    FamilyId = e.FamilyId,
                                    AniversaryDate = e.AniversaryDate,
                                    FamilyName = e.FamilyName,




                                }).ToList().OrderBy(e => e.FamilyName);
                return families;

            }

        }


        public bool AddParentstoFamily(AddParentToFamily model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var member = ctx.Members.Find(model.MemberId);
                var family = ctx.Families.Find(model.FamilyId);
                family.FamilyMember.Add(member);
                return ctx.SaveChanges() == 1;

            }
        }
        //public bool AddchildrenFamily(AddchildtoFamily model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {

        //        var family = ctx.Families.Find(model.FamilyId);
        //        var child = ctx.Children.Find(model.ChildId);
        //        family.FamilyMember.Add(child);

        //        return ctx.SaveChanges() == 1;

        //    }
        //}



        public FamilyDetails GetFamilyById(int familyId)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var family = ctx
                    .Families
                    .SingleOrDefault(f => f.FamilyId == familyId);



                return new FamilyDetails()
                {
                    FamilyId = family.FamilyId,
                    FamilyName = family.FamilyName,
                    AniversaryDate = family.AniversaryDate,
                    FamilyMembers = family.FamilyMember.Select(e => new PersonDetails()
                    {
                        FamilyMemberId = e.MemberId,
                        FirstName = e.FirstName,
                        LastName = e.LastName


                    }).ToList()

                };

            }
        }
        //public FamilyDetails GetFamilyById(int familyid)
        //{
        //    using(var ctx=new ApplicationDbContext())
        //    {
        //        var family = ctx.Families.Find(familyid);
        //        return new FamilyDetails()
        //        {
        //            FamilyId = family.FamilyId,
        //            AniversaryDate = family.AniversaryDate,
        //            FamilyName = family.FamilyName,
        //            Parent = ctx
        //                        .Members
        //                        .Where(e => e.MemberId == family.MemberId)
        //                        .Select(e => new ParentDetails
        //                        {
        //                              pa
        //                        })
        //        }
        //    }
        //}

    }
}

