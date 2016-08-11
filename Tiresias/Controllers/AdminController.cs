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
        public ActionResult CreateUser()
        {
            //ViewData["roles"] = new SelectList(dbContext.roles.OrderBy(n => n.role_id).ToList(), "role_id", "role_name");
            ViewData["roles"] = GetRolesDropDown();
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

        public SelectList GetRolesDropDown(int userId)            
        {
            var list  = new SelectList(
                from r in dbContext.roles.ToList()
                where r.role_id > 1
                select r,
                "role_id",
                "role_name",
                from r in dbContext.roles.ToList()
                join u in dbContext.users on r.role_id equals u.role_id
                where u.user_id == userId
                select r.role_name);
            return list;
        }

        public SelectList GetRolesDropDown()
        {
            var list = new SelectList(
                from r in dbContext.roles.ToList()
                where r.role_id > 1
                select r,
                "role_id",
                "role_name",
                from r in dbContext.roles.ToList()
                select r.role_name);
            return list;
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateUser(User newUser)
        {
            try
            {
                user dalSubmission = new user
                {
                    email = newUser.email,
                    password = newUser.password,
                    role_id = newUser.role_id
                };

                if (!dbContext.organizations.Any(o => o.orginization_name.Contains(newUser.OrgName)
                     || o.orginization_name.StartsWith(newUser.OrgName)))
                {
                    string orgAbrev = string.Join(string.Empty, newUser.OrgName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0]));
                    organization newOrg = new organization
                    {
                        orginization_name = newUser.OrgName,
                        orginization_abrev = orgAbrev
                    };
                    
                    dbContext.organizations.InsertOnSubmit(newOrg);                    
                    dbContext.SubmitChanges();
                    dalSubmission.organization_id = newOrg.organization_id;
                    
                }
                dbContext.users.InsertOnSubmit(dalSubmission);
                dbContext.SubmitChanges();

                return RedirectToAction("UserMGMT");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult EditUser(int id)
        {
            ViewData["roles"] = GetRolesDropDown(id);

            var userToEdit = (from u in dbContext.users
                              where u.user_id == id
                              join o in dbContext.organizations on u.organization_id equals o.organization_id
                              join r in dbContext.roles on u.role_id equals r.role_id
                              select new User
                              {
                                  email = u.email,
                                  RoleName = r.role_name,
                                  role_id = u.role_id,
                                  organization_id = u.organization_id,
                                  OrgName = o.orginization_name
                              }).FirstOrDefault();

            return View(userToEdit);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditUser(User userToSave)
        {
            try
            {
                user dalUser = (from u in dbContext.users
                                where u.user_id == userToSave.user_id
                                select u).FirstOrDefault();
                dalUser.email = userToSave.email;
                dalUser.password = userToSave.password;
                if (!dbContext.organizations.Any(o=>o.orginization_name.Contains(userToSave.OrgName)
                || o.orginization_name.StartsWith(userToSave.OrgName)))
                {
                    string orgAbrev = string.Join(string.Empty, userToSave.OrgName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0]));
                    organization newOrg = new organization
                    {
                        orginization_name = userToSave.OrgName,
                        orginization_abrev = orgAbrev
                    };

                    dbContext.organizations.InsertOnSubmit(newOrg);
                    dbContext.SubmitChanges();
                    dalUser.organization_id = userToSave.organization_id;
                }
                
                dalUser.role_id = userToSave.role_id;
                dbContext.SubmitChanges();

                return RedirectToAction("UserMGMT");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult DeleteUser(int id)
        {
            var user = (from u in dbContext.users
                        join o in dbContext.organizations on u.organization_id equals o.organization_id
                        join r in dbContext.roles on u.role_id equals r.role_id
                        where u.user_id == id
                        select new User
                        {
                            user_id = u.user_id,
                            email = u.email,
                            RoleName = r.role_name,
                            role_id = u.role_id,
                            organization_id = u.organization_id,
                            OrgName = o.orginization_name
                        }).FirstOrDefault();
                
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult DeleteUserConfirmed(int id)
        {
            try
            {
                var userToDelete = (from u in dbContext.users
                                    where u.user_id == id
                                    select u).FirstOrDefault();
                if (userToDelete.user_id == 1)
                {
                    return View("DeleteUser");
                }
                userToDelete.active = false;
                dbContext.SubmitChanges();

                return RedirectToAction("UserMGMT");
            }
            catch
            {
                return View("DeleteUser");
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
                         where u.active == true
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
