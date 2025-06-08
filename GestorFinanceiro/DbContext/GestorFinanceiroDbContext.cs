namespace GestorFinanceiro.DbContext
{
    public class GestorFinanceiroDbContext : DbContext
    {
        public GestorFinanceiroDbContext(DbContextOptions<GestorFinanceiroDbContext> options) : base(options) { }

        // Define DbSet properties for your entities here
        Public DbSet<YourEntity> YourEntities { get; set; }
    }
    {
    }
}
