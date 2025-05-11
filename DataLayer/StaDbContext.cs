using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public partial class StaDbContext : DbContext
{
    public StaDbContext()
    {
    }

    public StaDbContext(DbContextOptions<StaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicTerm> AcademicTerms { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Scholarship> Scholarships { get; set; }

    public virtual DbSet<ScholarshipModerator> ScholarshipModerators { get; set; }

    public virtual DbSet<ScholarshipOffering> ScholarshipOfferings { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AcademicTerm>(entity =>
        {
            entity.HasKey(e => e.TermId).HasName("PRIMARY");

            entity.ToTable("academicterm");

            entity.Property(e => e.TermName).HasMaxLength(6);
            entity.Property(e => e.Year).HasPrecision(4);

            entity.HasMany(d => d.Offerings).WithMany(p => p.Terms)
                .UsingEntity<Dictionary<string, object>>(
                    "ScholarshipCoveringTerm",
                    r => r.HasOne<ScholarshipOffering>().WithMany()
                        .HasForeignKey("OfferingId")
                        .HasConstraintName("FK_AcademicTerms_OfferingId"),
                    l => l.HasOne<AcademicTerm>().WithMany()
                        .HasForeignKey("TermId")
                        .HasConstraintName("FK_AcademicTerms_TermId"),
                    j =>
                    {
                        j.HasKey("TermId", "OfferingId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("scholarshipcoveringterm");
                        j.HasIndex(new[] { "OfferingId" }, "FK_AcademicTerms_OfferingId");
                    });
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PRIMARY");

            entity.ToTable("application");

            entity.HasIndex(e => e.OfferingId, "FK_Application_ScholarshipOffering");

            entity.HasIndex(e => e.StudentId, "FK_Application_Student");

            entity.HasOne(d => d.Offering).WithMany(p => p.Applications)
                .HasForeignKey(d => d.OfferingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Application_ScholarshipOffering");

            entity.HasOne(d => d.Student).WithMany(p => p.Applications)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Application_Student");
        });

        modelBuilder.Entity<ApplicationStatusHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PRIMARY");

            entity.ToTable("applicationstatushistory");

            entity.HasIndex(e => e.ApplicationId, "FK_ApplicationStatusHistory_Application");

            entity.HasIndex(e => e.ModeratorId, "FK_ApplicationStatusHistory_ScholarshipModerator");

            entity.Property(e => e.Status).HasMaxLength(15);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationStatusHistories)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ApplicationStatusHistory_Application");

            entity.HasOne(d => d.Moderator).WithMany(p => p.ApplicationStatusHistories)
                .HasForeignKey(d => d.ModeratorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ApplicationStatusHistory_ScholarshipModerator");
        });

        modelBuilder.Entity<Degree>(entity =>
        {
            entity.HasKey(e => e.DegreeTitle).HasName("PRIMARY");

            entity.ToTable("degree");

            entity.HasIndex(e => e.DepartmentTitle, "FK_Degree_Department");

            entity.Property(e => e.DegreeTitle).HasMaxLength(30);
            entity.Property(e => e.DepartmentTitle).HasMaxLength(30);

            entity.HasOne(d => d.DepartmentTitleNavigation).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.DepartmentTitle)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Degree_Department");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentTitle).HasName("PRIMARY");

            entity.ToTable("department");

            entity.Property(e => e.DepartmentTitle).HasMaxLength(30);
        });

        modelBuilder.Entity<Scholarship>(entity =>
        {
            entity.HasKey(e => e.ScholarshipId).HasName("PRIMARY");

            entity.ToTable("scholarship");

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.QueryEmail).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<ScholarshipModerator>(entity =>
        {
            entity.HasKey(e => e.ModeratorId).HasName("PRIMARY");

            entity.ToTable("scholarshipmoderator");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Role).HasMaxLength(15);
        });

        modelBuilder.Entity<ScholarshipOffering>(entity =>
        {
            entity.HasKey(e => e.OfferingId).HasName("PRIMARY");

            entity.ToTable("scholarshipoffering");

            entity.HasIndex(e => e.ScholarshipId, "FK_ScholarshipOffering_Scholarship");

            entity.Property(e => e.Status).HasMaxLength(15);

            entity.HasOne(d => d.Scholarship).WithMany(p => p.ScholarshipOfferings)
                .HasForeignKey(d => d.ScholarshipId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ScholarshipOffering_Scholarship");

            entity.HasMany(d => d.DegreeTitles).WithMany(p => p.Offerings)
                .UsingEntity<Dictionary<string, object>>(
                    "ScholarshipOfferingeligibility",
                    r => r.HasOne<Degree>().WithMany()
                        .HasForeignKey("DegreeTitle")
                        .HasConstraintName("FK_ScholarshipOfferingEligibility_Degree"),
                    l => l.HasOne<ScholarshipOffering>().WithMany()
                        .HasForeignKey("OfferingId")
                        .HasConstraintName("FK_ScholarshipOfferingEligibility_ScholarshipOffering"),
                    j =>
                    {
                        j.HasKey("OfferingId", "DegreeTitle")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("scholarshipofferingeligibility");
                        j.HasIndex(new[] { "DegreeTitle" }, "FK_ScholarshipOfferingEligibility_Degree");
                        j.IndexerProperty<string>("DegreeTitle").HasMaxLength(30);
                    });

            entity.HasMany(d => d.Moderators).WithMany(p => p.Offerings)
                .UsingEntity<Dictionary<string, object>>(
                    "ScholarshipOfferingmoderation",
                    r => r.HasOne<ScholarshipModerator>().WithMany()
                        .HasForeignKey("ModeratorId")
                        .HasConstraintName("FK_ScholarshipOfferingModeration_ScholarshipModerator"),
                    l => l.HasOne<ScholarshipOffering>().WithMany()
                        .HasForeignKey("OfferingId")
                        .HasConstraintName("FK_ScholarshipOfferingModeration_ScholarshipOffering"),
                    j =>
                    {
                        j.HasKey("OfferingId", "ModeratorId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("scholarshipofferingmoderation");
                        j.HasIndex(new[] { "ModeratorId" }, "FK_ScholarshipOfferingModeration_ScholarshipModerator");
                    });
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PRIMARY");

            entity.ToTable("student");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.DegreeTitle, "FK_Student_Degree");

            entity.Property(e => e.DegreeTitle).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(15);

            entity.HasOne(d => d.DegreeTitleNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.DegreeTitle)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Student_Degree");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
