using System.Linq;
using System.Web.Mvc;
using UniForum.Models;

namespace UniForum.Controllers
{
    //This Controller is only authorized to Admin
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ForumEntities db1 = new ForumEntities();
        Admin admin = new Admin();
        // GET: Admin
        /// <summary>
        /// Getting All Posts That are Approved
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// This action Method is For Approving Students
        /// </summary>
        /// <returns></returns>
        public ActionResult ApproveStudent()
        {
            return View(admin.ApproveStudent());
        }
        /// <summary>
        /// Getting All Favorite Posts
        /// </summary>
        /// <returns></returns>
        public ActionResult FavPost()
        {
            return View();
        }
        /// <summary>
        /// This Action is Use For Approving Posts
        /// </summary>
        /// <returns></returns>
        public ActionResult ApprovePost()
        {
            return View(admin.ApprovePost());
         }

        /// <summary>
        /// this Action is Used For Approving Students
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            
            if (admin.UpdateUser(id)==1)
            {


                return Json(new { status = true, message = "Updated SuccessFully Succesfully" }, JsonRequestBehavior.AllowGet);
            }

                return Json(new { status = false, message = "Not Updated SuccessFully Succesfully" }, JsonRequestBehavior.AllowGet);

        }
    }


}