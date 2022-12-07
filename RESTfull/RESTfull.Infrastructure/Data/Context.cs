using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;

namespace RESTfull.Infrastructure.Data
{
  public class Context : DbContext
  {
    public Context(DbContextOptions<Context> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
  }
}
