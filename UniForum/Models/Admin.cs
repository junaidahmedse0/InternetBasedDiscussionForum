using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniForum.Models
{
    public class Admin
    {

        //Method for Using To ApproveStudent
        public List<User> ApproveStudent()
        {
            using (var db = new ForumEntities())
            {
                var users = db.Users.Where(x => x.Status == false).ToList();
                return users;
            }

        }
        //Method for Using To ApprovePosts
        public List<Post> ApprovePost()
        {
            using (var db = new ForumEntities())
            {
                var posts = db.Posts.Where(x => x.Status == false).ToList();
                return posts;
            }

        }
        //Update User 
        public int UpdateUser(int id)
        {
            using (var db = new ForumEntities())
            {
                var user = db.Users.Find(id);
                user.Status = true;

                return db.SaveChanges();
            }
        }
    }
}