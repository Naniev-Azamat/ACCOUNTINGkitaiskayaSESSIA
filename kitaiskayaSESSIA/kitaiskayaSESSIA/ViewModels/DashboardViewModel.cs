using System.Globalization;
using kitaiskayaSESSIA.Models;

namespace kitaiskayaSESSIA.ViewModels
{
    public class DashboardViewModel
    {
        public IReadOnlyList<Transaction> Transactions { get; set; } = Array.Empty<Transaction>();

        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Balance => Income - Expense;

        public bool IsAllTime { get; set; }
        public DateOnly SelectedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public bool IsDirector { get; set; }
        public int CurrentUserId { get; set; }
        public AddTransactionViewModel NewTransaction { get; set; } = new();
        public string ScopeLabel => IsAllTime ? "(за всё время)" : "(за выбранный день)";

        private static readonly CultureInfo Ru = CultureInfo.GetCultureInfo("ru-RU");
        public static string FormatMoney(decimal value) => value.ToString("#,##0.##", Ru) + " ₽";
    }
}
