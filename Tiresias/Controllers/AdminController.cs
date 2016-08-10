using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Tiresias.DAL;
using Tiresias.Models;

namespace Tiresias.Controllers
{
    public class AdminController : Controller
    {
        public tiresiasDBcontextDataContext dbContext = new tiresiasDBcontextDataContext();


        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Message = "Admin Page";
            
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            var myEntry = dbContext.submissions
                     .Where(c => c.submission_id == id)
                     .Select(s => new Submission
                     {
                         submission_id = s.submission_id,
                         submission_date = s.submission_date,
                         submission_content = s.submission_content,
                         submission_email = s.submission_email,
                         work_id = s.work_id,
                         approved = s.approved,
                         editor_id = s.editor_id
                     })
                     .SingleOrDefault();

            return View(myEntry);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/Approvals
        public ActionResult Approvals()
        {
            var myPendingEntry = dbContext.submissions
                   .Where(c => c.approved == false)
                   .Where(a=> a.active == true)
                   .Select(s => new Submission
                   {
                       submission_id = s.submission_id,
                       submission_date = s.submission_date,
                       submission_content = s.submission_content,
                       submission_email = s.submission_email,
                       work_id = s.work_id,
                       approved = s.approved,
                       editor_id = s.editor_id
                   });

            return View(myPendingEntry);
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Approve(int id)
        {
            var submissionToApprove = (from s in dbContext.submissions
                                       where s.submission_id == id
                                       select s).FirstOrDefault();
            submissionToApprove.approved = true;
            dbContext.SubmitChanges();
            return RedirectToAction("Approvals");
        }

        public ActionResult Reject(int id)
        {
            var submissionToApprove = (from s in dbContext.submissions
                                       where s.submission_id == id
                                       select s).FirstOrDefault();
            submissionToApprove.approved = false;
            submissionToApprove.active = false;
            dbContext.SubmitChanges();
            return RedirectToAction("Approvals");
        }


        public ActionResult UserMGMT()
        {
            var users = (from u in dbContext.users
                         join o in dbContext.organizations on u.organization_id equals o.organization_id
                         join r in dbContext.roles on u.role_id equals r.role_id
                         select new User
                         {
                             email = u.email,
                             organization_id = u.organization_id,
                             password = u.password,
                             role_id = u.role_id,
                             user_id = u.user_id,
                             RoleName = r.role_name,
                             OrgName = o.orginization_name
                         });
            byte[] data; 
            foreach (var u in users)
            {
                data = System.Text.Encoding.ASCII.GetBytes(u.password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                String hash = System.Text.Encoding.ASCII.GetString(data);
                u.password = hash;
            }
            

            return View(users);
        }

    }
}
