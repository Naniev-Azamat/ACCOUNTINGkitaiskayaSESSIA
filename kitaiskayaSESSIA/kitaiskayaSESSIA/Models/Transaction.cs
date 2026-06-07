using System.ComponentModel.DataAnnotations;

namespace kitaiskayaSESSIA.Models
{
    public class Transaction
    {
        public long Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        public DateOnly Date { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
