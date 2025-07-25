namespace GestorFinanceiro.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<IncomeModel> Incomes { get; set; }
        public ICollection<IncomeModel> Expenses { get; set; }
    }
}
