using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;


namespace MVC.Controllers
{

    public class EmpDataController : Controller
    {
        public ViewResult AllEmployees()
        {
            var context = new Mydatabase();
            var model = context.Employees.ToList();
            return View(model);
        }

        public ViewResult Find(string id)
        {
            int empId = int.Parse(id);
            var context = new Mydatabase();
            var model = context.Employees.FirstOrDefault((e) => e.EmpID == empId);
            return View(model);

        }
      
        [HttpPost]
        public ActionResult Find(Employee emp)
        {
            var context = new Mydatabase();
            var model = context.Employees.FirstOrDefault((e) => e.EmpID == emp.EmpID);
            model.EmpName = emp.EmpName;
            model.EmpAddress = emp.EmpAddress;
            model.EmpSalary = emp.EmpSalary;
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }

        public ViewResult NewEmployee()
        {
            var model = new Mydatabase();
            return View(model);
        }
        public ActionResult NewEmployee(Employee emp)
        {
            try
            {
                var context = new Mydatabase();
                context.Employees.Add(emp);
                context.SaveChanges();
                return RedirectToAction("AllEmployees");
            }
            catch
            {
                return RedirectToAction("AllEmployees");
            }
        }
        public ActionResult Delete(string id)
        {
            int empId = int.Parse(id);
            var context = new Mydatabase();
            var model = context.Employees.FirstOrDefault((e) => e.EmpID == empId);
            context.Employees.Remove(model);
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }
    }
}