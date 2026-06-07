using System.ComponentModel.DataAnnotations;

namespace kitaiskayaSESSIA.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = UserRole.Employee;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
