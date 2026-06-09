using kitaiskayaSESSIA.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace kitaiskayaSESSIA.Data
{
    public class JsonDataContext
    {
        private readonly string _usersPath;
        private readonly string _transactionsPath;

        public List<User> Users { get; }
        public List<Transaction> Transactions { get; }

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = { new JsonStringEnumConverter() }
        };

        public JsonDataContext(IConfiguration configuration, IWebHostEnvironment env)
        {
            var folder = configuration["Storage:DataDirectory"] ?? "App_Data";
            if (!Path.IsPathRooted(folder))
                folder = Path.Combine(env.ContentRootPath, folder);
            Directory.CreateDirectory(folder);

            _usersPath = Path.Combine(folder, "users.json");
            _transactionsPath = Path.Combine(folder, "transactions.json");

            Users = Load<User>(_usersPath);
            Transactions = Load<Transaction>(_transactionsPath);
        }

        public void SaveUsers() => Save(_usersPath, Users);

        public void SaveTransactions() => Save(_transactionsPath, Transactions);

        private static List<T> Load<T>(string path)
        {
            if (!File.Exists(path))
                return new List<T>();

            var json = File.ReadAllText(path);
            if (json == "")
                return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(json, JsonOptions) ?? new List<T>();
        }

        private static void Save<T>(string path, List<T> items)
        {
            var json = JsonSerializer.Serialize(items, JsonOptions);
            File.WriteAllText(path, json);
        }
    }
}
