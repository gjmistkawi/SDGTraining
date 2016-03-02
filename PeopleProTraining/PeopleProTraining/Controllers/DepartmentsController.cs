using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PeopleProTraining.Dal.Infrastructure;
using PeopleProTraining.Dal.Models;

namespace PeopleProTraining.Controllers
{
    public class DepartmentsController : Controller
    {
        private PeopleProRepo p_repo = new PeopleProRepo();

        // GET: Departments
        public ActionResult Index()
        {
            return View(p_repo.GetDepartments().ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = p_repo.GetDepartment(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,DepartmentCode,StaffCount,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                p_repo.SaveDepartment(department);
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = p_repo.GetDepartment(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentCode,StaffCount,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                p_repo.SaveDepartment(department);
                department.StaffCount = 0;
                return RedirectToAction("Index");
            }
            return View(department);
        }

        //GET: Department
        public ActionResult Delete(int? id)
        {
            if(!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Department department = p_repo.GetDepartment(id.Value);


            return View(department);
        }

        //POST: Department/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            //deletes if no employees are related
            if (!p_repo.DeleteDepartment(p_repo.GetDepartment(id)))
            {
                //if employees, reassign employees

            }

            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                p_repo.Dispose(null);
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult CreateAjax(Department department)
        {
            //if (department.DepartmentName == null || department.DepartmentName.Length < 2)
            //{
            //    return new HttpStatusCodeResult(420, "Department names must be at least 3 characters long.");
            //}

            if (!ModelState.IsValid)
            {
                return PartialView("_ErrorMessage", department);
            }

            else
            {
                p_repo.SaveDepartment(department);
                department.StaffCount = 0;
            }
            return PartialView("_DepartmentRow", department);
        }
    }
}
