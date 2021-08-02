using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.MemberFolder;
using WinnersIndy.Services;
using System.Net;
using System.Net.Mail;
using WinnersIndy.Common;
using WinnersIndy.Model.FamilyModel;

namespace WinnersIndy.Controllers
{
    public class MemberController : Controller
    {
        [Authorize]
        private MemberServices CrteateMemberService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new MemberServices(userid);
            return service;
        }
        //This is to enable access to family list  and to be able to select family when adding  member
        private FamilyServices CreateFamilyService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new FamilyServices(userid);
            return service;
        }
        // GET: Member
        public ActionResult Index()
        {
            var service = CrteateMemberService();
            var memlist = service.GetMembers();
            return View(memlist);
        }
        //GET:Member/create
        public ActionResult Create()
        {
            var service = CreateFamilyService();

            MemberCreate model = new MemberCreate();
           //List<SelectListItem>  Items = new SelectList(service.GetFamilies(), "FamilyId", "FamilyName",model.FamilyId).ToList();
           // Items.Insert(0, (new SelectListItem { Text = "SelectFamily", Value = "0" }));
           // ViewBag.MyList = Items;
            List<FamilyListItem> FamilyItem = service.GetFamilies().ToList();
            FamilyListItem family = new FamilyListItem() { FamilyId = 0, FamilyName = "Select Family" };
            FamilyItem.Add(family);
            ViewBag.MyList = new SelectList( FamilyItem.OrderBy(x=>x.FamilyId).ToList(),"FamilyId","FamilyName");
            return View();
            
        }
        //POST:Member/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CrteateMemberService();
            if (model.FamilyId == 0)
            {
                model.FamilyId = null;
            }
            if (service.CreateMember(model))
            {
                TempData["SaveResult"] = "Member added to the church Directory";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Member could not be added");
            return View(model);
        }
        //=======================Get Member Details=============
        public ActionResult Details(int Id)
        {

            var service = CrteateMemberService();
            MemberDetails model = service.GetMemberById(Id);
            return View(model);
        }
        //GET:Restaurant/Edit
        public ActionResult Edit(int id)
        {
            
            var service = CrteateMemberService();
            var model = service.GetMemberById(id);
            var modelupdate = new MemberEdit()
            {
                MemberId = model.MemberId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                EmailAddress = model.EmailAddress,
                FileContent = model.FileContent,



            };
            return View(modelupdate);
        }
        //===================Edit Members================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberEdit model, int id)
        {

            if (!ModelState.IsValid) return View(model);
            if (model.MemberId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var services = CrteateMemberService();
            if (services.UpdateMember(model))
            {
                TempData["SaveResult"] = "Member's Information updated succesfully.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Update Failed.");
            return View(model);
        }
        // GET :Member/Delete{Id}
        public ActionResult Delete(int id)
        {
            var sevice = CrteateMemberService();
            var model = sevice.GetMemberById(id);
            return View(model);

        }
        //GET :Member/Delete/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var sevice = CrteateMemberService();
            sevice.DeleteMember(id);
            TempData["SaveResult"] = "Member  was deleted ";
            return RedirectToAction("Index");
        }
        //GET:GetChildren
        public ActionResult GetChildren()
        {
            var service = CrteateMemberService();
            return View(service.GetChildren());

        }

        public ActionResult SendBulkEmail(Email model)
        {

            return RedirectToAction("Email");
        }

        //GET:AddChildToClass

        public ActionResult AddChildToClass(int id)
        {
            var service = CrteateMemberService();
            var model = new AddChild
            {
                MemberId = id,
                ChildrenClasses = service.GetDropdown(),
            };
            return View(model);
        }
        //POST:AddChildToClass

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddChildToClass(AddChild model)
        {
            if (!ModelState.IsValid) return View();
            var services = CrteateMemberService();
            if (services.AddChildtoclass(model))
            {
                TempData["SaveResult"] = "Child Succsfully Added to Class ";
                return RedirectToAction("Index", "ChildrenClass");
            }

            ModelState.AddModelError("", "Child Could not be Added");

            return View(model);
        }
        //GET:GetTeachers
        public ActionResult GetTeachers()
        {
            var services = CrteateMemberService();
            return View(services.GetTeachers());
        }

        //GET:AddTeacherToClass
        public ActionResult AddTeacherToClass(int id)
        {
            var service = CrteateMemberService();
            var model = new AddChild
            {
                MemberId = id,
                ChildrenClasses = service.GetDropdown(),
            };
            return View(model);
        }
        //POST:AddTeacherToClass

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddTeacherToClass(AddChild model)
        {
            if (!ModelState.IsValid) return View();
            var services = CrteateMemberService();
            if (services.AddChildtoclass(model))
            {
                TempData["SaveResult"] = "Teacher Succsfully Added to Class ";
                return RedirectToAction("Index", "ChildrenClass");
            }

            ModelState.AddModelError("", "Teacher Could not be Added");

            return View(model);
        }
        public ActionResult GetFamilyMember(int id)
        {
            var services = CrteateMemberService();
            var familymember = services.GetFamilyMember(id);
            if (familymember is null)
            {
                return HttpNotFound();
            }
            return View(familymember);
        }
       
       
        //============= Send Email ====================//
        public ActionResult SendEmail(int id)
        {

            var services = CrteateMemberService();
            var member = services.GetMemberById(id);
            string from = "foluso.o.adegboye@gmail.com";
            string messg = String.Format($"Dear  {member.FirstName}<br/>\n" +
                $"Turnaround greetings to you in the name of the Lord Jesus christ<br/>\n" +
                $"We just like to reach out to yoiuy and find out that you are not in church yesterday");
            string subject = "Absent From Church"; 
            SendEmail sendEmail = new SendEmail();
            sendEmail.SendNotification( member.EmailAddress, messg,subject);
            

                return RedirectToAction("Index");
        }
        //GET:Email
        public ActionResult Email(Email model)
        {
            if (model.Id != null)
            {
                ViewBag.CheckId = "true";
            }
            else
            {
                ViewBag.CheckId = null;
            }
            return View();
        }
        //POST:Email

        [HttpPost]
        [ActionName("Email")]
        public ActionResult IndividualEmail2(Email model)
        {
            var services = CrteateMemberService();
            string sendTo = string.Empty;
            if (model.Id!=null)
            {
                var member = services.GetMemberById(Convert.ToInt32(model.Id));
                sendTo = member.EmailAddress;
            }
            else
            {
                sendTo = model.To;
            }
            
            SendEmail SendEmail = new SendEmail();
           
            
            try
            {
                if (ModelState.IsValid)
                {

                    SendEmail.SendNotification(sendTo, model.Body, model.Subject);
                    TempData["SaveResult"] = "Email was successfuly sent.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
                return View();
            
        }

        //public void SendBirthdayMessage()
        //{
        //    var services = CrteateMemberService();
        //    var members = services.GetMembers();
        //    foreach(var member in members)
        //    {
        //        if (member.DateOfBirth == DateTime.Today)
        //        {
        //            SendEmail(member.MemberId);
        //        }
        //    }
        //}



    }


}


