using Microsoft.EntityFrameworkCore;

namespace RocklogicTransactions
{
    public class TransactionContext(DbContextOptions<TransactionContext> options) : DbContext(options)
    {
        public DbSet<Transaction> Transactions { get; set; }
    }
}