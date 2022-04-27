using DapperDemo.Models;
using DapperDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DapperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBonusRepository bonusRepository;
        public HomeController(ILogger<HomeController> logger, IBonusRepository bonusRepo)
        {
            _logger = logger;
            bonusRepository = bonusRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Company> companies = bonusRepository.GetAllCompanyWithEmployees();
            return View(companies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}