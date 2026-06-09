using kitaiskayaSESSIA.Data;
using kitaiskayaSESSIA.Models;
using Microsoft.Extensions.Options;

namespace kitaiskayaSESSIA.Services
{
    public class UserService : IUserService
    {
        private readonly JsonDataContext _data;
        private readonly AuthOptions _auth;

        public UserService(JsonDataContext data, IOptions<AuthOptions> auth)
        {
            _data = data;
            _auth = auth.Value;
        }

        public User? Authenticate(string name, string password)
        {
            var displayName = name.Trim();
            var login = displayName.ToLower();

            if (login == "" || password == "")
                return null;

            if (login == _auth.DirectorName.ToLower())
            {
                if (password != _auth.DirectorPassword)
                    return null;

                return GetDirector();
            }

            if (password != _auth.EmployeePassword)
                return null;

            var user = _data.Users.FirstOrDefault(u => u.Name == login);

            if (user == null)
            {
                user = new User
                {
                    Id = NextId(),
                    Name = login,
                    DisplayName = displayName,
                    Password = _auth.EmployeePassword,
                    Role = UserRole.Employee
                };
                _data.Users.Add(user);
                _data.SaveUsers();
            }

            return user;
        }

        public User? FindByName(string name)
        {
            var login = name.Trim().ToLower();
            return _data.Users.FirstOrDefault(u => u.Name == login);
        }

        public void EnsureDirector()
        {
            GetDirector();
        }

        private User GetDirector()
        {
            var login = _auth.DirectorName.ToLower();
            var director = _data.Users.FirstOrDefault(u => u.Name == login);

            if (director == null)
            {
                director = new User
                {
                    Id = NextId(),
                    Name = login,
                    DisplayName = _auth.DirectorDisplayName,
                    Password = _auth.DirectorPassword,
                    Role = UserRole.Director
                };
                _data.Users.Add(director);
                _data.SaveUsers();
            }

            return director;
        }

        private int NextId()
        {
            if (_data.Users.Count == 0)
                return 1;

            return _data.Users.Max(u => u.Id) + 1;
        }
    }
}
