using System;
using Tiresias.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiresias.DAL;
using Nelibur.ObjectMapper;

namespace Tiresias.Controllers
{
    public class EntriesController : Controller
    {
        public tiresiasDBcontextDataContext dbContext = new tiresiasDBcontextDataContext();

        // GET: Entries
        public ActionResult Index()
        {
            var myEntries = (from s in dbContext.submissions
                             join w in dbContext.works on s.work_id equals w.work_id
                             join a in dbContext.authors on w.author_id equals a.author_id
                             join u in dbContext.users on s.editor_id equals u.user_id
                             where s.approved == true && s.active == true
                             select new Submission
                             {
                                 submission_id = s.submission_id,
                                 submission_date = s.submission_date,
                                 submission_content = s.submission_content,
                                 submission_email = s.submission_email,
                                 work_id = s.work_id,
                                 work_title = w.title,
                                 author_name = a.first_name + " " + a.last_name,
                                 approved = s.approved,
                                 editor_id = s.editor_id,
                                 editor_email = u.email
                             });

            return View(myEntries);
        }

        // GET: Entries/Details/5
        public ActionResult Details(int id)
        {
            var myEntry = (from s in dbContext.submissions
                           join w in dbContext.works on s.work_id equals w.work_id
                           join a in dbContext.authors on w.author_id equals a.author_id
                           join u in dbContext.users on s.editor_id equals u.user_id
                           where s.submission_id == id &&  s.approved == true
                           select new Submission
                           {
                               submission_id = s.submission_id,
                               submission_date = s.submission_date,
                               submission_content = s.submission_content,
                               submission_email = s.submission_email,
                               work_id = s.work_id,
                               work_title = w.title,
                               author_name = a.first_name + " " + a.last_name,
                               approved = s.approved,
                               editor_id = s.editor_id,
                               editor_email =u.email
                           }).FirstOrDefault();

            return View(myEntry);
        }

        // GET: Entries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entries/Create
        [HttpPost]
        public ActionResult Create(Submission formToInsert)
        {
            try
            {
                submission dalSubmission = new submission
                {
                    submission_date = DateTime.Now,
                    work_id = formToInsert.work_id,
                    submission_content = formToInsert.submission_content,
                    editor_id = formToInsert.editor_id,
                    approved = false
                };

                // TODO: get userID
                var currentUserIDbyEmail = (from u in dbContext.users
                                           where u.email.Contains(formToInsert.submission_email)
                                           select u.email).FirstOrDefault();

                dalSubmission.submission_email = currentUserIDbyEmail;     // user id




                dbContext.submissions.InsertOnSubmit(dalSubmission);
                dbContext.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Entries/Edit/5
        public ActionResult Edit(int id)
        {
            var myEntry = (from s in dbContext.submissions
                           join w in dbContext.works on s.work_id equals w.work_id
                           join a in dbContext.authors on w.author_id equals a.author_id
                           join u in dbContext.users on s.editor_id equals u.user_id
                           where s.submission_id == id && s.approved == true
                           select new Submission
                           {
                               submission_id = s.submission_id,
                               submission_date = s.submission_date,
                               submission_content = s.submission_content,
                               submission_email = s.submission_email,
                               work_id = s.work_id,
                               work_title = w.title,
                               author_name = a.first_name + " " + a.last_name,
                               approved = s.approved,
                               editor_id = s.editor_id,
                               editor_email = u.email
                           }).FirstOrDefault();
            return View(myEntry);
        }

        // POST: Entries/Edit/5
        [HttpPost]
        public ActionResult Edit(Submission submissionToInsert)
        {
            try
            {
                submission dalSubmission = (from s in dbContext.submissions
                                     where s.submission_id == submissionToInsert.submission_id
                                     select s).FirstOrDefault();
                dalSubmission.submission_date = DateTime.Now;
                dalSubmission.submission_email = submissionToInsert.submission_email;
                dalSubmission.work_id = submissionToInsert.work_id;
                dalSubmission.submission_content = submissionToInsert.submission_content;
                dalSubmission.editor_id = submissionToInsert.editor_id; // need to write methods to get all kinds of ids
                dalSubmission.approved = false; // submit changes for approval
                dbContext.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Entries/Delete/5
        public ActionResult Delete(int id)
        {
            var myEntry = (from s in dbContext.submissions
                           join w in dbContext.works on s.work_id equals w.work_id
                           join a in dbContext.authors on w.author_id equals a.author_id
                           join u in dbContext.users on s.editor_id equals u.user_id
                           where s.submission_id == id && s.approved == true
                           select new Submission
                           {
                               submission_id = s.submission_id,
                               submission_date = s.submission_date,
                               submission_content = s.submission_content,
                               submission_email = s.submission_email,
                               work_id = s.work_id,
                               work_title = w.title,
                               author_name = a.first_name + " " + a.last_name,
                               approved = s.approved,
                               editor_id = s.editor_id,
                               editor_email = u.email
                           }).FirstOrDefault();
            return View(myEntry);
        }

        // POST: Entries/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var entryToDelete = (from s in dbContext.submissions
                                     where s.submission_id == id
                                     select s).FirstOrDefault();
                entryToDelete.active = false;
                dbContext.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public bool IsAuthorizedToEdit()
        {
            // return true if user's role_id between 1 and 3 
            return true;
        }


    }
}
