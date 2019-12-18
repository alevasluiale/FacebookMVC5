using daw.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace daw.Controllers
{
    public class CreatePostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        [Authorize(Roles = "Administrator,RegistredUser")]
        public ActionResult CreatePost(Post post, HttpPostedFileBase[] files)
        {
            try
            {
                post.UserId = User.Identity.GetUserId();
                post.UserName = User.Identity.GetUserName();
                db.Posts.Add(post);
                db.SaveChanges();
                if (files != null)
                {
                    foreach (HttpPostedFileBase file in files)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + file.FileName);
                        Image img = new Image();
                        img.Path = "Images/" + file.FileName;
                        img.PostId = db.Posts.Max(item => item.PostId);
                        db.Images.Add(img);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("List","Post");
            }
            catch (Exception e)
            {
                return RedirectToAction("List", "Post");
            }
        }
    }
}