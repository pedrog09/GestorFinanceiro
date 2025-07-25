namespace GestorFinanceiro.Models
{
    public class IncomeModel
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int Category { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
