using kitaiskayaSESSIA.Models;
using System.ComponentModel.DataAnnotations;

namespace kitaiskayaSESSIA.ViewModels
{
    public class AddTransactionViewModel
    {
        [Required(ErrorMessage = "Укажите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата операции")]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required(ErrorMessage = "Введите описание")]
        [MaxLength(300)]
        [Display(Name = "Описание")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Тип")]
        public TransactionType Type { get; set; } = TransactionType.Income;

        [Required(ErrorMessage = "Введите сумму")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше нуля")]
        [Display(Name = "Сумма (₽)")]
        public decimal Amount { get; set; }
    }
}
