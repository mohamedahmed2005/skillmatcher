using Microsoft.EntityFrameworkCore;
using SkillMatch.API.Core.Entities;

namespace SkillMatch.API.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

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
    }
}
