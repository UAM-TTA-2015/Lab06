using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UamTTA.Model;

namespace UamTTA.Storage
{
    public class UamTTAContext : DbContext
    {
        public UamTTAContext() : base("UamTTAContext")
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<BudgetTemplate> BudgetTemplates { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AccountConfiguration());
        }

        public sealed class AccountConfiguration : EntityTypeConfiguration<Account>
        {
            public AccountConfiguration()
            {
                HasKey(x => x.Id);
            }
        }
    }
}