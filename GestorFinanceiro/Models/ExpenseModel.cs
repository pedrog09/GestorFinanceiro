namespace GestorFinanceiro.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public UserModel User { get; set; }
        public CategoryModel Category { get; set; }
    }
}
