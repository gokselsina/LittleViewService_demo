using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace littleviewservice.Models
{
    public class LittleContext : DbContext
    {
        public LittleContext(DbContextOptions<LittleContext> options) : base(options) 
        {

        }
        public DbSet<Little> Littles { get; set; }
        public DbSet<Account> tbl_account { get; set; }
        public DbSet<Student> tbl_student { get; set; }
    }
}
