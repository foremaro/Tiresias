using System;
using Tiresias.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiresias.DAL;

namespace Tiresias.Controllers
{
    public class BlogController : Controller
    {
        public tiresiasDBcontextDataContext dbContext = new tiresiasDBcontextDataContext();


        // GET: Blog
        public ActionResult Index()
        {

            var myBlogs = from p in dbContext.posts
                          orderby p.post_date descending
                          select new Post
                          {
                              post_id = p.post_id,
                              post_title = p.post_title,
                              post_type = p.post_type,
                              post_date = p.post_date,
                              post_content = p.post_content
                          };

            return View(myBlogs);
        }

        // GET: Blog/Details/5
        public ActionResult Article(int id)
        {
            var myArticle = dbContext.posts
                .Where(c => c.post_id == id)
                .Select(p=> new Post
                {
                    post_id = p.post_id,
                    post_title = p.post_title,
                    post_type = p.post_type,
                    post_date = p.post_date,
                    post_content = p.post_content
                })
                .SingleOrDefault();

            return View(myArticle);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
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

        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Blog/Edit/5
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

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
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
