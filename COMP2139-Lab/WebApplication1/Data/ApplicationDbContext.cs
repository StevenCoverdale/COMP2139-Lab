using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Areas.ProjectManagement.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Project>Projects { get; set; }
    
    public DbSet<ProjectTask>Tasks{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

       
        modelBuilder.Entity<Project>()
            .Property(p => p.StartDate)
            .HasConversion(
                v => v.ToUniversalTime(),  
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));  

        modelBuilder.Entity<Project>()
            .Property(p => p.EndDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        modelBuilder.Entity<Project>().HasMany(p => p.Tasks).WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId).OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Project>().HasData(
            new Project { ProjectId = 1, Name = "Assignment1", Description = "Comp2139_Assignment1", StartDate = new DateTime(2025, 1, 13, 6, 32, 24, DateTimeKind.Utc), EndDate = new DateTime(2025, 2, 26, 12, 0, 0, DateTimeKind.Utc) },
            new Project { ProjectId = 2, Name = "Assignment2", Description = "Comp2139_Assignment2", StartDate = new DateTime(2025, 2, 16, 9, 23, 42, DateTimeKind.Utc), EndDate = new DateTime(2025, 3, 31, 12, 0, 0, DateTimeKind.Utc) }
        );

        // modelBuilder.Entity<Project>().HasData(
        //     new Project {ProjectId = 1, Name = "Assignment1", Description = "Comp2139_Assignment1"},
        //     new Project {ProjectId = 2, Name = "Assignment2", Description = "Comp2139_Assignment2"}
        //     );
    }
    
}