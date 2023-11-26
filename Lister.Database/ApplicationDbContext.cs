using Lister.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Lister.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<UserTestHistory> UserTestsHistory { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}