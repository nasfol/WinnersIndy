using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.MemberFolder;
using WinnersIndy.Services;

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
            return View();
        }
        //POST:Member/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CrteateMemberService();
            if (service.CreateMember(model))
            {
                TempData["SaveResult"] = "Member added to the church Directory";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Member could not be added");
            return View();
        }
        public ActionResult Details(int Id)
        {

            var service = CrteateMemberService();
            var model = service.GetMemberById(Id);
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
                FileContent = model.FileContent


            };
            return View(modelupdate);
        }
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

    }
}
