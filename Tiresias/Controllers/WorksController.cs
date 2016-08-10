using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiresias.DAL;
using Tiresias.Models;

namespace Tiresias.Controllers
{
    public class WorksController : Controller
    {
        public tiresiasDBcontextDataContext dbContext = new tiresiasDBcontextDataContext();

        // GET: Works
        public ActionResult Index()
        {
            var myWorks = from w in dbContext.works
                          join a in dbContext.authors on w.author_id equals a.author_id
                          join l in dbContext.languages on w.language_id equals l.language_id
                          join t in dbContext.translators on w.translator_id equals t.translator_id
                          join u in dbContext.users on w.user_entry_Id equals u.user_id
                          join m in dbContext.metadatas on w.metadata_id equals m.metadata_id
                          where w.active == true
                          orderby w.title
                          select new Work
                          {
                              work_id = w.work_id,
                              title = w.title,
                              edition = w.edition,
                              author_id = w.author_id,
                              author_name = a.first_name + " " + a.last_name,
                              language_id = w.language_id,
                              language = l.language_name,
                              translator_id = w.translator_id,
                              translator_name = t.translator_name,
                              user_entry_id = w.user_entry_Id,
                              user_entry_email = u.email,
                              metadata_id = w.metadata_id,
                              media_type = m.media_type,
                              doi = m.doi,
                              isbn = m.isbn
                              
                          };

            return View(myWorks);
        }

        // GET: Works/Details/5
        public ActionResult Details(int id)
        {
            var myWork = (from w in dbContext.works
                          join a in dbContext.authors on w.author_id equals a.author_id
                          join l in dbContext.languages on w.language_id equals l.language_id
                          join t in dbContext.translators on w.translator_id equals t.translator_id
                          join u in dbContext.users on w.user_entry_Id equals u.user_id
                          join m in dbContext.metadatas on w.metadata_id equals m.metadata_id
                          where w.work_id == id 
                          select new Work
                          {
                              work_id = w.work_id,
                              title = w.title,
                              edition = w.edition,
                              author_id = w.author_id,
                              author_name = a.first_name + " " + a.last_name,
                              language_id = w.language_id,
                              language = l.language_name,
                              translator_id = w.translator_id,
                              translator_name = t.translator_name,
                              user_entry_id = w.user_entry_Id,
                              user_entry_email = u.email,
                              metadata_id = w.metadata_id,
                              media_type = m.media_type,
                              doi = m.doi,
                              isbn = m.isbn
                          }).FirstOrDefault();                

            return View(myWork);
        }

        // GET: Works/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Works/Create
        [HttpPost]
        public ActionResult Create(Work workToInsert)
        {
            
            try
            {
                work dalSubmission = new work
                {
                    title = workToInsert.title,
                    edition = workToInsert.edition
                };

                // insert new author here
                var authorsFullName = workToInsert.author_name;
                var names = authorsFullName.Split(' ');
                string firstName = names[0];
                string lastName = names[1];
                
                if (!dbContext.authors.Any((n => n.first_name.Contains(firstName)
                || n.last_name.Contains(lastName)
                || n.first_name.StartsWith(firstName)
                || n.last_name.StartsWith(lastName))))
                {
                    author newAuthor = new author
                    {
                        first_name = firstName,
                        last_name = lastName
                    };
                    dbContext.authors.InsertOnSubmit(newAuthor);
                    dbContext.SubmitChanges();
                    dalSubmission.author_id = newAuthor.author_id; // author id
                }

                // insert new language
                if (!dbContext.languages.Any(l=>l.language_name.Contains(workToInsert.language)))
                {
                    language newLang = new language
                    {
                        language_name = workToInsert.language
                    };
                    dbContext.languages.InsertOnSubmit(newLang);
                    dbContext.SubmitChanges();
                    dalSubmission.language_id = newLang.language_id; // language id
                }

                // insert new translator AND ORGANIZATION
                // REMEMBER TO INCLUDE ORGANIZATION ON CREATION VIEW!
                if (!dbContext.translators.Any(t => t.translator_name.Contains(workToInsert.translator_name)
                     || t.translator_name.StartsWith(workToInsert.translator_name)))
                {

                    if (!dbContext.organizations.Any(o => o.orginization_name.Contains(workToInsert.organization_name)
                     || o.orginization_name.StartsWith(workToInsert.organization_name)))
                    {
                        string orgAbrev = string.Join(string.Empty, workToInsert.organization_name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0]));
                        organization newOrg = new organization
                        {
                            orginization_name = workToInsert.organization_name,
                            orginization_abrev = orgAbrev
                        };
                        dbContext.organizations.InsertOnSubmit(newOrg);
                        dbContext.SubmitChanges();

                        translator newTranslator = new translator
                        {
                            translator_name = workToInsert.translator_name,
                            organization_id = newOrg.organization_id
                        };

                        dbContext.translators.InsertOnSubmit(newTranslator);
                        dbContext.SubmitChanges();
                        dalSubmission.translator_id = newTranslator.translator_id;  // translator id
                    }
                }


                // insert user who entered
                // TODO: get current user
                var currentUserIDbyEmail = (from u in dbContext.users
                                         where u.email.Contains(workToInsert.user_entry_email)
                                         select u.user_id).FirstOrDefault();

                dalSubmission.user_entry_Id = currentUserIDbyEmail;     // user id

                // insert new metadata stuff
                metadata newMetaData = new metadata
                {
                    media_type = workToInsert.media_type,
                    doi = workToInsert.doi,
                    isbn = workToInsert.isbn
                };
                dbContext.metadatas.InsertOnSubmit(newMetaData);
                dbContext.SubmitChanges();
                dalSubmission.metadata_id = newMetaData.metadata_id;    // metadata id

                // insert new record into works                
                dbContext.works.InsertOnSubmit(dalSubmission);               
                dbContext.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Works/Edit/5
        public ActionResult Edit(int id)
        {
            var myWork = (from w in dbContext.works
                          join a in dbContext.authors on w.author_id equals a.author_id
                          join l in dbContext.languages on w.language_id equals l.language_id
                          join t in dbContext.translators on w.translator_id equals t.translator_id
                          join u in dbContext.users on w.user_entry_Id equals u.user_id
                          join m in dbContext.metadatas on w.metadata_id equals m.metadata_id
                          join o in dbContext.organizations on t.organization_id equals o.organization_id
                          where w.work_id == id
                          select new Work
                          {
                              work_id = w.work_id,
                              title = w.title,
                              edition = w.edition,
                              author_id = w.author_id,
                              author_name = a.first_name + " " + a.last_name,
                              language_id = w.language_id,
                              language = l.language_name,
                              translator_id = w.translator_id,
                              translator_name = t.translator_name,
                              user_entry_id = w.user_entry_Id,
                              organization_name = o.orginization_name, 
                              user_entry_email = u.email,
                              metadata_id = w.metadata_id,
                              media_type = m.media_type,
                              doi = m.doi,
                              isbn = m.isbn
                          }).FirstOrDefault();

            return View(myWork);
        }

        // POST: Works/Edit/5
        [HttpPost]
        public ActionResult Edit(Work workToUpdate)
        {
            try
            {
                // TODO: Add update logic here
                work dalWork = (from w in dbContext.works
                                where w.work_id == workToUpdate.work_id
                                select w).FirstOrDefault();

                dalWork.title = workToUpdate.title;
                dalWork.author_id = workToUpdate.author_id;
                dalWork.edition = workToUpdate.edition;                
                dalWork.language_id = workToUpdate.language_id;
                dalWork.translator_id = workToUpdate.translator_id;
                dalWork.metadata_id = workToUpdate.metadata_id;
                dalWork.user_entry_Id = workToUpdate.user_entry_id;
                dbContext.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Works/Delete/5
        public ActionResult Delete(int id)
        {
            var myWork = (from w in dbContext.works
                          join a in dbContext.authors on w.author_id equals a.author_id
                          join l in dbContext.languages on w.language_id equals l.language_id
                          join t in dbContext.translators on w.translator_id equals t.translator_id
                          join u in dbContext.users on w.user_entry_Id equals u.user_id
                          join m in dbContext.metadatas on w.metadata_id equals m.metadata_id
                          join o in dbContext.organizations on t.organization_id equals o.organization_id
                          where w.work_id == id
                          select new Work
                          {
                              work_id = w.work_id,
                              title = w.title,
                              edition = w.edition,
                              author_id = w.author_id,
                              author_name = a.first_name + " " + a.last_name,
                              language_id = w.language_id,
                              language = l.language_name,
                              translator_id = w.translator_id,
                              translator_name = t.translator_name,
                              user_entry_id = w.user_entry_Id,
                              organization_name = o.orginization_name,
                              user_entry_email = u.email,
                              metadata_id = w.metadata_id,
                              media_type = m.media_type,
                              doi = m.doi,
                              isbn = m.isbn
                          }).FirstOrDefault();

            return View(myWork);
        }

        // POST: Works/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var workToDelete = (from w in dbContext.works
                                    where w.work_id == id
                                    select w).FirstOrDefault();
                workToDelete.active = false;
                dbContext.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
