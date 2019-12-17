using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using daw.Models;

namespace daw.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            var posts = from post in db.Posts
                        orderby post.PostId
                        select post;
            foreach(Post post in posts)
            {
                post.Images = db.Images.Where(img => img.PostId == post.PostId).ToList();
            }
            foreach (Post post in posts)
            {
                post.Comments = db.Comments.Where(comm => comm.PostId == post.PostId).ToList();
            }

            ViewBag.Posts = posts;
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles= "Administrator,RegistredUser")]
        public ActionResult Create(Post post, HttpPostedFileBase[] files)
        {
            try
            {
                post.UserId = User.Identity.GetUserId();
                post.UserName = User.Identity.GetUserName();
                db.Posts.Add(post);
                db.SaveChanges();
                if (files != null)
                {
                    foreach(HttpPostedFileBase file in files)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Post/Images/")
                                                          + file.FileName);
                        Image img = new Image();
                        img.Path = "Images/" + file.FileName;
                        img.PostId = db.Posts.Max(item => item.PostId);
                        db.Images.Add(img);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                return List();
            }
        }
        [Authorize(Roles = "Administrator,RegistredUser")]
        public ActionResult Delete(int id)
        {

            Post post = db.Posts.Find(id);
            if (User.IsInRole("Administrator") || post.UserId == User.Identity.GetUserId())
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            else return View();
        }

        public ActionResult Edit(int id)
        {

            return View(db.Posts.Find(id));
        }
        public ActionResult Details(int id)
        {

            return View(db.Posts.Find(id));
        }
        public ActionResult AddPostLike(int id)
        {
            try
            {
                Like like = new Like();
                like.UserId = Int32.Parse(User.Identity.GetUserId());
                like.PostId = id;
                db.Likes.Add(like);
                db.SaveChanges();
                return List();
            }
            catch
            {
                return List();
            }
        }
        
    }
}