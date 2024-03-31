using SquadCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SquadCrud.Models;

namespace MVCPractical14.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpPost]
        public JsonResult Index(string search)
        {
            using (var context = new squadDBEntities())
            {
                var searchedEmployees = context.Employees.Where(e => e.FirstName.Contains(search)).ToList();
                return Json(searchedEmployees);
            }
        }  
        //dvvdvdfv

        //[HttpGet]
        //public ActionResult Index(int page = 1)
        //{
        //    int recordsPerPage = 10;
        //    using (var context = new squadDBEntities())
        //    {
                //var pageResult = context.Employees.ToList().ToPagedList(page, recordsPerPage);
                //return View(pageResult);
        //    }
        //}

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if(ModelState.IsValid)
            {
                using (var context = new squadDBEntities())
                {
                    context.Employees.Add(emp);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            using(var context = new squadDBEntities())
            {
                var getEmployee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
                return View(getEmployee);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var context = new squadDBEntities())
            {
                var getEmployee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
                return View(getEmployee);
            }
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                using (var context = new squadDBEntities())
                {
                    var getEmployee = context.Employees.Where(e => e.Id == emp.Id).FirstOrDefault();
                    getEmployee.FirstName = emp.FirstName;
                    getEmployee.LastName = emp.LastName;
                    getEmployee.Designation = emp.Designation;
                    getEmployee.Department = emp.Department;
                    getEmployee.Salary = emp.Salary;
                    getEmployee.JoiningDate = getEmployee.JoiningDate;
                    getEmployee.ReportingPerson = getEmployee.ReportingPerson;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using(var context = new squadDBEntities())
            {
                var getEmployee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
                return View(getEmployee);
            }
        }

        [HttpPost]
        public ActionResult Delete(Employee emp)
        {
            using(var context = new squadDBEntities())
            {
                var deletedEmployee = context.Employees.Where(e => e.Id == emp.Id).FirstOrDefault();
                context.Employees.Remove(deletedEmployee);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}