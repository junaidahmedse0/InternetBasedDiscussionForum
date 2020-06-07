using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using UniForum.ViewModel;

namespace UniForum.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var db = new ForumEntities())
            {

                bool IsTrue = db.Users.Any(x => x.Email == model.Email && x.Status == true);
                if (IsTrue)
                {
                    bool IsValid = db.Users.Any(x => x.Email == model.Email && x.Password == model.Password && x.Status == true);

                    if (IsValid)
                    {

                        var result = (from user in db.Users
                                      join role in db.UserRoles on user.Id equals role.UserId
                                      where user.Email == model.Email
                                      select role.Roles).ToArray();


                        if (result.Contains("admin"))
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            return RedirectToAction("Index", "Admin");

                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            return RedirectToAction("Index", "Posts");
                        }
                    }
                }
                //else
                {
                    ModelState.AddModelError("", "Your Account Is Not Approved");

                    return View();
                }

            }
            ModelState.AddModelError("", "Invalid username and password");

            return View();

        }
        public ActionResult SignUp()
        {
            //return View();
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserViewModel model)
        {

            User user = new User();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            user.Email = model.Email;
            user.Password = model.Password;
            user.Contact = model.Contact;
            UserRole userRole = new UserRole();
            userRole.Roles = model.UserRole.Roles;
            userRole.UserId = model.Id;

            using (var context = new ForumEntities())
            {
                context.Users.Add(user);
                context.UserRoles.Add(userRole);

                context.SaveChanges();
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("admin"))
                    {
                        return RedirectToAction("Index", "Admin");

                    }
                }


                return RedirectToAction("Login");
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}