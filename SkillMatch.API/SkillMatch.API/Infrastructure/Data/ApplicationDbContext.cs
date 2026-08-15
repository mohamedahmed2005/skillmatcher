using Microsoft.EntityFrameworkCore;
using SkillMatch.API.Core.Entities;

namespace SkillMatch.API.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<CandidateProfile> CandidateProfiles { get; set; }
    public DbSet<CompanyProfile> CompanyProfiles { get; set; }
    public DbSet<JobPosting> JobPostings { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<ResumeDocument> ResumeDocuments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // CandidateProfile -> JobApplications
        modelBuilder.Entity<JobApplication>()
            .HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey(x => x.CandidateProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        // JobPosting -> JobApplications
        modelBuilder.Entity<JobApplication>()
            .HasOne(x => x.JobPosting)
            .WithMany(x => x.Applications)
            .HasForeignKey(x => x.JobPostingId)
            .OnDelete(DeleteBehavior.NoAction);

        // Salary precision
        modelBuilder.Entity<JobPosting>()
            .Property(x => x.SalaryMin)
            .HasPrecision(18, 2);

        modelBuilder.Entity<JobPosting>()
            .Property(x => x.SalaryMax)
            .HasPrecision(18, 2);
    }
}