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
    public class CompaniesController : Controller
    {

        private readonly IEmployeeRepository employeeRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IBonusRepository bonusRepository;
        private readonly IDapperSprocRepo dapperRepoSproc;
        public CompaniesController(ICompanyRepository repository, IEmployeeRepository employee, IBonusRepository bonusRepo, IDapperSprocRepo dapperproc)
        {
            employeeRepository = employee;
            companyRepository = repository;
            bonusRepository = bonusRepo;
            dapperRepoSproc = dapperproc;
        }
        // GET: Companies
        public async Task<IActionResult> Index()
        {
            //return View(companyRepository.GetAll());
            return View(dapperRepoSproc.List<Company>("usp_GetALLCompany"));
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = bonusRepository.GetCompanyWithEmployees(id.GetValueOrDefault());
                // companyRepository.Find(id.GetValueOrDefault());
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,Name,Address,City,State,PostalCode")] Company company)
        {
            ModelState.Remove("Employees");
            if (ModelState.IsValid)
            {
                companyRepository.Add(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var company = companyRepository.Find(id.GetValueOrDefault());
            var company = dapperRepoSproc.Single<Company>("usp_GetCompany", new { CompanyId = id.GetValueOrDefault()});
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,Name,Address,City,State,PostalCode")] Company company)
        {
            if (id != company.CompanyId)
            {
                return NotFound();
            }
            ModelState.Remove("Employees");
            if (ModelState.IsValid)
            {
                try
                {
                   companyRepository.Update(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            companyRepository.Remove(id.GetValueOrDefault());
            
            return RedirectToAction(nameof(Index));
        }

    }
}
