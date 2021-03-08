using Microsoft.AspNetCore.Mvc;
using Paychecks.Models;
using Paychecks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paychecks.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var employees = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel 
            {
                Id = employee.Id,
                EmployeeNumber = employee.EmployeeNo,
                ImageUrl = employee.ImageUrl,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Designation = employee.Designation,
                City = employee.City,
                DateJoined = employee.DateJoined
            }).ToList();
            return View(employees);
        }
    }
}
