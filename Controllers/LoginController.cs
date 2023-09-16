using AuthenticationAndAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var userRegistrationViewModal = new UserRegistrationViewModal();

            using (WeltecMainEntities weltecMainEntities = new WeltecMainEntities())
            {
                userRegistrationViewModal.RoleData = weltecMainEntities.RoleMasters.Select(y =>
                new SelectListItem
                {
                    Text = y.RoleName,
                    Value = y.RoleID.ToString()

                }).ToList();
            }


            return View(userRegistrationViewModal);
        }

        [HttpPost]
        public ActionResult Index(UserRegistrationViewModal userRegistrationViewModal)
        {
            AuthenticationAndAuthorization.User user = new AuthenticationAndAuthorization.User();

            user.Name = userRegistrationViewModal.Name;
            user.RoleID = userRegistrationViewModal.RoleId;
            user.EmaiIID = userRegistrationViewModal.EmailID;
            user.PasswordSalt = Helper.GeneratePassword(10);
            user.PasswordHash = Helper.EncodePassword(userRegistrationViewModal.Password, user.PasswordSalt);

            using (WeltecMainEntities db = new WeltecMainEntities())
            {
                db.Users.Add(user); 
                db.SaveChanges();   
            }


                return View();
        }

   
        
        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModal loginViewModal)
        {

            

            using (WeltecMainEntities weltecMainEntities = new WeltecMainEntities())
            {
                var user = weltecMainEntities.Users.Where(y => y.EmaiIID == loginViewModal.Email).FirstOrDefault();


                var password = Helper.EncodePassword(loginViewModal.Password,user.PasswordSalt);

                var d = new List<string>();

                if(user.PasswordHash.Equals(password))
                {
                    d.Add(user.RoleMaster.RoleName);
                    Helper.CreateAuthCookie(user.ID, user.Name, user.Name, user.EmaiIID, user.PasswordHash, d.ToArray(), true);
                    return RedirectToAction("Index", "Home");
                }


            }

           return View(loginViewModal);
        }


    }
}