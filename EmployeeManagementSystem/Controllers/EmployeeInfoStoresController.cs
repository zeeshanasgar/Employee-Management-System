using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseLibrary;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class EmployeeInfoStoresController : Controller
    {
        private EmployeeDBEntities db = new EmployeeDBEntities();
        [Authorize(Roles = "admin, hr")]
        public ActionResult Index()
        {
            return View(db.EmployeeInfoStore.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInfoStore employeeInfoStore = db.EmployeeInfoStore.Find(id);
            if (employeeInfoStore == null)
            {
                return HttpNotFound();
            }
            return View(employeeInfoStore);
        }
        [Authorize(Roles = "admin, hr")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, hr")]
        public ActionResult Create([Bind(Include = "EmpId,Name,username,password,Address,Phone,Email,TotalSalary,empCode")] EmployeeInfoStore employeeInfoStore)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeInfoStore.Add(employeeInfoStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeInfoStore);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInfoStore employeeInfoStore = db.EmployeeInfoStore.Find(id);
            if (employeeInfoStore == null)
            {
                return HttpNotFound();
            }
            return View(employeeInfoStore);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpId,Name,username,password,Address,Phone,Email,TotalSalary,empCode")] EmployeeInfoStore employeeInfoStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeInfoStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeInfoStore);
        }
        [Authorize(Roles = "admin, hr")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInfoStore employeeInfoStore = db.EmployeeInfoStore.Find(id);
            if (employeeInfoStore == null)
            {
                return HttpNotFound();
            }
            return View(employeeInfoStore);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, hr")]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeInfoStore employeeInfoStore = db.EmployeeInfoStore.Find(id);
            db.EmployeeInfoStore.Remove(employeeInfoStore);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
