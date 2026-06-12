using kitaiskayaSESSIA.Data;
using kitaiskayaSESSIA.Models;
using kitaiskayaSESSIA.ViewModels;

namespace kitaiskayaSESSIA.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly JsonDataContext _data;

        public TransactionService(JsonDataContext data)
        {
            _data = data;
        }

        public DashboardViewModel BuildDashboard(int currentUserId, bool isDirector, DateOnly? date, bool allTime)
        {
            var selectedDate = date ?? DateOnly.FromDateTime(DateTime.Today);
            var list = _data.Transactions.AsEnumerable();
            if (!isDirector)
                list = list.Where(t => t.UserId == currentUserId);

            if (!allTime)
                list = list.Where(t => t.Date == selectedDate);

            var items = list.OrderByDescending(t => t.Id).ToList();
            decimal income = 0;
            decimal expense = 0;
            foreach (var t in items)
            {
                if (t.Type == TransactionType.Income)
                    income += t.Amount;
                else
                    expense += t.Amount;
            }

            return new DashboardViewModel
            {
                Transactions = (IReadOnlyList<System.Transactions.Transaction>)items,
                Income = income,
                Expense = expense,
                IsAllTime = allTime,
                SelectedDate = selectedDate,
                IsDirector = isDirector,
                CurrentUserId = currentUserId,
                NewTransaction = new AddTransactionViewModel { Date = selectedDate }
            };
        }

        public void Add(AddTransactionViewModel model, User currentUser)
        {
            var tx = new Transaction
            {
                Id = NextId(),
                UserId = currentUser.Id,
                UserName = currentUser.DisplayName,
                Date = model.Date,
                Description = model.Description.Trim(),
                Type = model.Type,
                Amount = model.Amount,
                CreatedAt = DateTime.UtcNow
            };

            _data.Transactions.Add(tx);
            _data.SaveTransactions();
        }

        public bool Delete(long id, int currentUserId, bool isDirector)
        {
            var tx = _data.Transactions.FirstOrDefault(t => t.Id == id);
            if (tx == null)
                return false;
            if (!isDirector && tx.UserId != currentUserId)
                return false;

            _data.Transactions.Remove(tx);
            _data.SaveTransactions();
            return true;
        }

        private long NextId()
        {
            if (_data.Transactions.Count == 0)
                return 1;

            return _data.Transactions.Max(t => t.Id) + 1;
        }
    }
}
