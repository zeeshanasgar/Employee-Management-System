using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLibrary;
using EmployeeManagementSystem.Models;
using System.Web.Security;

namespace EmployeeManagementSystem.Controllers
{
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult Login(Models.Membership model)
        //{
        //    using (var context = new EmployeeDBEntities())
        //    {
        //        bool isValid = context.EmployeeInfoStore.Any(x=>x.username == model.username && x.password == model.password);
        //        if (isValid)
        //        {
        //            FormsAuthentication.SetAuthCookie(model.username, false);
        //            return RedirectToAction("Index", "EmployeeInfoStores");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid username or Password");
        //        }
        //    }
        //    return View();
        //}

        public ActionResult Login(Models.Membership model)
        {
            using (var context = new EmployeeDBEntities())
            {
                var user = context.EmployeeInfoStore.FirstOrDefault(x => x.username == model.username && x.password == model.password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.username, false);

                    // Retrieve the user's role
                    var roles = Roles.GetRolesForUser(model.username);

                    if (roles.Contains("admin"))
                    {
                        // Redirect to the admin detail page
                        return RedirectToAction("Details", "EmployeeInfoStores", new { id = user.EmpId });
                    }
                    else if (roles.Contains("hr"))
                    {
                        // Redirect to the HR detail page
                        return RedirectToAction("Details", "EmployeeInfoStores", new { id = user.EmpId });
                    }
                    else
                    {
                        // Redirect to the employee detail page
                        return RedirectToAction("Details", "EmployeeInfoStores", new { id = user.EmpId });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or Password");
                }
            }
            return View();
        }



        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(EmployeeInfoStore model)
        {
            using (var context = new EmployeeDBEntities())
            {
                context.EmployeeInfoStore.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}