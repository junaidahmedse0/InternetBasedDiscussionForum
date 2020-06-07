using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UniForum.ViewModel;

namespace UniForum.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ForumEntities db = new ForumEntities();

        // GET: Posts
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllPosts()
        {

            var posts = (from p in db.Posts
                 
                        join u in db.Users on p.UserId equals u.Id
                        where p.Status == true
                        select new PostViewModel{ FirstName = u.FirstName, Post = p }).ToList();

            
            //return PartialView("PostsPartial", db.Posts.Where(x => x.Status == true).OrderBy(x=>x.Favorite).ToList());
            return PartialView("PostsPartial",posts);
        }
        public ActionResult AllFav()
        {
            var posts = (from p in db.Posts

                         join u in db.Users on p.UserId equals u.Id
                         where p.Status == true && p.Favorite==true
                         select new PostViewModel { FirstName = u.FirstName, Post = p }).ToList();
            return PartialView("PostsPartial",posts);
        }


        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return Json(post, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Comments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ForumEntities())
            {
                var comments = db.Comments.Where(x => x.PostId == id).Select(x => x.Comment1).ToList();

         

                var c = (from x in db.Comments where x.PostId == id
                         join u in db.Users on x.UserId equals u.Id
                         select new { u.FirstName, x.Comment1 }).ToList();


                if (comments == null)
                {
                    return HttpNotFound();
                }
                return Json(c, JsonRequestBehavior.AllowGet);
            }



        }


        
        [HttpPost]
        public ActionResult Create(Post post)
        {

            var userid = User.Identity.Name;
            var user = db.Users.Where(x => x.Email == userid).FirstOrDefault();
            var p = new Post();
            p.Question = post.Question;
            p.Subject = post.Subject;
            p.Description = post.Description;
            p.UserId = user.Id;
            db.Posts.Add(p);
            db.SaveChanges();
            return Json(new { status = true, message = "Successfully Added" }, JsonRequestBehavior.AllowGet);


        }

       
     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
         }

        //
        public ActionResult Update(int id)
        {
            using (var db = new ForumEntities())
            {
                var post = db.Posts.Find(id);
                post.Status = true;

                db.SaveChanges();
                return Json(new { status = true, message = "Updated Succesfully" }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            using (var db = new ForumEntities())
            {
                Comment c = new Comment();
                var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
                c.Comment1 = comment.Comment1;
                c.PostId = comment.PostId;
                c.UserId = user.Id;

                db.Comments.Add(c);
                db.SaveChanges();



                return Json(new { status = true, message = "Added Succesfully" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult AddLike(int id)
        {
            using (var db = new ForumEntities())
            {
                var post = db.Posts.Find(id);
                post.Likes += 1;

                db.SaveChanges();
                return Json(new { status = true, message = "You Like This" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult AddFav(int id)
        {
            using (var db = new ForumEntities())
            {
                var post = db.Posts.Find(id);
                post.Favorite =true;

                db.SaveChanges();
                return Json(new { status = true, message = "Your Favorite" }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
