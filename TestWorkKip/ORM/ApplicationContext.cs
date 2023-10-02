using Microsoft.EntityFrameworkCore;
using TestWorkKip.Entities;

namespace TestWorkKip.ORM
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Query> Queries { get; set; }
        public DbSet<Result> Results { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();

            Database.EnsureCreated();
        }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
       
        

    }
}
