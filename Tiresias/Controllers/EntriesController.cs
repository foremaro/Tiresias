using System;
using Tiresias.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiresias.DAL;

namespace Tiresias.Controllers
{
    public class EntriesController : Controller
    {
        public tiresiasDBcontextDataContext dbContext = new tiresiasDBcontextDataContext();

        // GET: Entries
        public ActionResult Index()
        {
            var myEntries = from s in dbContext.submissions
                          select new Submission
                          {
                              submission_id  = s.submission_id,
                              submission_date = s.submission_date,
                              submission_content = s.submission_content,
                              submission_email = s.submission_email,
                              work_id = s.work_id,
                              approved = s.approved,
                              editor_id = s.editor_id
                          };

            return View(myEntries);
        }

        // GET: Entries/Details/5
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

        // GET: Entries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entries/Create
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

        // GET: Entries/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Entries/Edit/5
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

        // GET: Entries/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Entries/Delete/5
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
    }
}
