#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DapperDemo.DbContexts;
using DapperDemo.Models;
using DapperDemo.Repositories;

namespace DapperDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IBonusRepository bonusRepository;
        [BindProperty]
        public Employee Employee { get; set; }
        public EmployeesController(ICompanyRepository repository, IEmployeeRepository employee, IBonusRepository bonusRepo)
        {
            employeeRepository = employee;
            companyRepository = repository;
            bonusRepository = bonusRepo;
        }

        public async Task<IActionResult> Index(int companyId = 0)
        {
            //List<Employee> employees = employeeRepository.GetAll();
            //foreach (var employee in employees)
            //{
            //    employee.Company = companyRepository.Find(employee.CompanyId);
            //}
            List<Employee> employees = bonusRepository.GetEmployeeWithCompany(companyId);
            return View(employees);
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var company = employeeRepository.Find(id.GetValueOrDefault());
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(company);
        //}

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> companyList = companyRepository.GetAll().Select(i => new SelectListItem 
            { 
                Text = i.Name,
                Value = i.CompanyId.ToString()
            }
            );
            ViewBag.CompanyList = companyList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            ModelState.Remove("Company");
            if (ModelState.IsValid)
            {
                employeeRepository.Add(Employee);
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> companyList = companyRepository.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            }
            );
            ViewBag.CompanyList = companyList;

            Employee = employeeRepository.Find(id.GetValueOrDefault());
            if (Employee == null)
            {
                return NotFound();
            }
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employeeRepository.Update(Employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            employeeRepository.Remove(id.GetValueOrDefault());
            
            return RedirectToAction(nameof(Index));
        }

    }
}
