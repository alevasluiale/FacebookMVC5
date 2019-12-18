using daw.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace daw.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Comment
        [HttpPost]
        public ActionResult NewComment(Comment comment)
        {
            try
            {
                comment.UserName = User.Identity.GetUserName();
                comment.UserId = User.Identity.GetUserId();
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("List","Post");
            }
            catch
            {
                return RedirectToAction("List","Post");
            }
        }
       
    }
}