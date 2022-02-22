using Microsoft.EntityFrameworkCore;

namespace library.models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=SchoolDatabase;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entitiy =>
            {
                entitiy.Property(e => e.StudentNumber).ValueGeneratedNever();
            }
            );
        }
    }
}
