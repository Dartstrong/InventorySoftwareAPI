using Microsoft.EntityFrameworkCore;
namespace KGTA_project.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options){ }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
    }
}
