using Microsoft.EntityFrameworkCore;
using musicSchool.Domain.Entities;

namespace musicSchool.Infrastructure.Persistence;

public class MusicSchoolDbContext : DbContext
{
    public MusicSchoolDbContext(DbContextOptions<MusicSchoolDbContext> options) : base(options) { }

    public DbSet<School> Schools => Set<School>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentTeacher> StudentTeachers => Set<StudentTeacher>();
    public DbSet<StudentSchool> StudentSchools => Set<StudentSchool>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<School>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Code).IsUnique();
            b.Property(x => x.Code).HasMaxLength(50).IsRequired();
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<Teacher>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Identifier).IsUnique();
            b.Property(x => x.Identifier).HasMaxLength(50).IsRequired();
            b.Property(x => x.FirstName).IsRequired();
            b.Property(x => x.LastName).IsRequired();

            b.HasOne(x => x.School)
             .WithMany(s => s.Teachers)
             .HasForeignKey(x => x.SchoolId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Student>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Identifier).IsUnique();
            b.Property(x => x.Identifier).HasMaxLength(50).IsRequired();
            b.Property(x => x.FirstName).IsRequired();
            b.Property(x => x.LastName).IsRequired();
            b.Property<DateOnly>("DateOfBirth").IsRequired();
        });

        modelBuilder.Entity<StudentTeacher>(b =>
        {
            b.HasKey(x => new { x.StudentId, x.TeacherId });
            b.HasOne(st => st.Student).WithMany(s => s.StudentTeachers).HasForeignKey(st => st.StudentId);
            b.HasOne(st => st.Teacher).WithMany(t => t.StudentTeachers).HasForeignKey(st => st.TeacherId);
        });

        modelBuilder.Entity<StudentSchool>(b =>
        {
            b.HasKey(x => new { x.StudentId, x.SchoolId });
            b.HasOne(ss => ss.Student).WithMany(s => s.StudentSchools).HasForeignKey(ss => ss.StudentId);
            b.HasOne(ss => ss.School).WithMany(s => s.StudentSchools).HasForeignKey(ss => ss.SchoolId);
        });
    }
}
