namespace GestorFinanceiro.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Income" ou "Expense"
        public int? UserId { get; set; } // NULL para categorias do sistema
        public bool IsSystem { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        
        // Navigation properties
        public UserModel User { get; set; }
    }
} 