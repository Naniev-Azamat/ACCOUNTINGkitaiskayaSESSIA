using kitaiskayaSESSIA.Models;
using kitaiskayaSESSIA.ViewModels;

namespace kitaiskayaSESSIA.Services
{
    public interface ITransactionService
    {
        DashboardViewModel BuildDashboard(int currentUserId, bool isDirector, DateOnly? date, bool allTime);
        void Add(AddTransactionViewModel model, User currentUser);
        bool Delete(long id, int currentUserId, bool isDirector);
    }
}
