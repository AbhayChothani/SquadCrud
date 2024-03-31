using PagedList;
using SquadCrud.Models;
using SquadCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPractical14.Controllers
{
    public class PracticalController : Controller
    {
        [HttpPost]
        public JsonResult Index(string search)
        {
            if(search != "")
            {
                using (var context = new squadDBEntities())
                {
                    var searchedEmployees = 
                        context.Employees.Where(e => e.FirstName.StartsWith(search)).ToList();
                    if(searchedEmployees.Count == 0)
                    {
                        searchedEmployees = context.Employees.Where(e=>e.LastName.StartsWith(search)).ToList();
                    }
                    if(searchedEmployees.Count == 0)
                    {
                        searchedEmployees = context.Employees.Where(e=>e.Department.StartsWith(search)).ToList();
                    }
                    return Json(searchedEmployees);
                }
            }
            else
            {
                using (var context = new squadDBEntities())
                {
                    var searchedEmployees = context.Employees.ToList();
                    return Json(searchedEmployees);
                }
            }
            
        }

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            int recordsPerPage = 5;
            using (var context = new squadDBEntities())
            {
                var pageResult = context.Employees.ToList().ToPagedList(page, recordsPerPage);
                return View(pageResult);
            }

           
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
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
            using (var context = new squadDBEntities())
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
                    getEmployee.JoiningDate = emp.JoiningDate;
                    getEmployee.ReportingPerson = emp.ReportingPerson;
                    getEmployee.City = emp.City;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var context = new squadDBEntities())
            {
                var getEmployee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
                return View(getEmployee);
            }
        }

        [HttpPost]
        public ActionResult Delete(Employee emp)
        {
            using (var context = new squadDBEntities())
            {
                var deletedEmployee = context.Employees.Where(e => e.Id == emp.Id).FirstOrDefault();
                context.Employees.Remove(deletedEmployee);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}