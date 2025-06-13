namespace GestorFinanceiro.Models
{
    public class IncomeModel
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Notes { get; set; }
        public int UsuarioId { get; set; }
        public User User { get; set; }
    }
}
