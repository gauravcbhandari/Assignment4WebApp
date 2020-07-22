using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Controllers
{
        public class EmployeeController : Controller
    {
        IEmployeeRepository _repo;
        public EmployeeController(IEmployeeRepository Repo)
        {
            _repo = Repo;
        }

        public IActionResult Index()
        {
            List<Employee> employeelist = Task.Run(() => _repo.GetAllEmployee()).Result;

            return View(employeelist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee e)
        {
            if (ModelState.IsValid)
            {
                var result = _repo.SaveEmployee(e);

                return RedirectToAction("index", "Employee");
            }

            return View();
        }
    }
}