using kitaiskayaSESSIA.Models;
using kitaiskayaSESSIA.Services;
using kitaiskayaSESSIA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace kitaiskayaSESSIA.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITransactionService _transactions;
        public DashboardController(ITransactionService transactions)
        {
            _transactions = transactions;
        }

        [HttpGet]
        public IActionResult Index(DateOnly? date, bool allTime = false)
        {
            var vm = _transactions.BuildDashboard(CurrentUserId, IsDirector, date, allTime);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddTransactionViewModel model, bool allTime = false)
        {
            if (!ModelState.IsValid)
            {
                var vm = _transactions.BuildDashboard(CurrentUserId, IsDirector, model.Date, allTime);
                vm.NewTransaction = model;
                return View(nameof(Index), vm);
            }
            _transactions.Add(model, CurrentUser);
            return RedirectToAction(nameof(Index), new { date = model.Date, allTime });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id, DateOnly? date, bool allTime = false)
        {
            var ok = _transactions.Delete(id, CurrentUserId, IsDirector);
            if (!ok)
                TempData["Error"] = "Вы можете удалять только свои записи";

            return RedirectToAction(nameof(Index), new { date, allTime });
        }

        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        private bool IsDirector => User.IsInRole(UserRole.Director);

        private User CurrentUser => new()
        {
            Id = CurrentUserId,
            DisplayName = User.FindFirstValue(ClaimTypes.Name) ?? "Пользователь",
            Role = IsDirector ? UserRole.Director : UserRole.Employee
        };
    }
}
