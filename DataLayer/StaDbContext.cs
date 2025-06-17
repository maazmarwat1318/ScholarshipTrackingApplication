
using DataLayer.Entity;
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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AcademicTerm>(entity =>
        {
            entity.HasKey(e => e.TermId).HasName("PRIMARY");
            entity.ToTable("AcademicTerm");
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
            entity.ToTable("ApplicationStatusHistory");
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
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("degree");

            entity.HasIndex(e => e.DepartmentId, "FK_Degree_Department"); // ✅ Updated to DepartmentId

            entity.Property(e => e.DegreeTitle).HasMaxLength(30);

            entity.HasOne(d => d.Department).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Degree_Department");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

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
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("scholarshipmoderator");
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Moderator).WithOne(p => p.ScholarshipModerator)
                .HasForeignKey<ScholarshipModerator>(d => d.Id)
                .HasConstraintName("FK_Moderator_Id");
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

            entity.HasMany(d => d.Degrees).WithMany(p => p.Offerings) // ✅ Changed from DegreeTitles to Degrees
                .UsingEntity<Dictionary<string, object>>(
                    "ScholarshipOfferingEligibility",
                    r => r.HasOne<Degree>().WithMany()
                        .HasForeignKey("DegreeId") // ✅ Updated to DegreeId
                        .HasConstraintName("FK_ScholarshipOfferingEligibility_Degree"),
                    l => l.HasOne<ScholarshipOffering>().WithMany()
                        .HasForeignKey("OfferingId")
                        .HasConstraintName("FK_ScholarshipOfferingEligibility_ScholarshipOffering"),
                    j =>
                    {
                        j.HasKey("OfferingId", "DegreeId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("scholarshipofferingeligibility");
                        j.HasIndex(new[] { "DegreeId" }, "FK_ScholarshipOfferingEligibility_Degree");
                    });

            entity.HasMany(d => d.Moderators).WithMany(p => p.Offerings)
                .UsingEntity<Dictionary<string, object>>(
                    "ScholarshipOfferingModeration",
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
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("student");
            entity.HasIndex(e => e.DegreeId, "FK_Student_Degree");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Degree).WithMany(p => p.Students)
                .HasForeignKey(d => d.DegreeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Student_Degree");

            entity.HasOne(d => d.StudentNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.Id)
                .HasConstraintName("FK_Student_Id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("user");
            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(15);
        });

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
