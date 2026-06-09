using kitaiskayaSESSIA.Models;

namespace kitaiskayaSESSIA.Services
{
    public interface IUserService
    {
        User? Authenticate(string name, string password);
        void EnsureDirector();
        User? FindByName(string name);
    }
}
