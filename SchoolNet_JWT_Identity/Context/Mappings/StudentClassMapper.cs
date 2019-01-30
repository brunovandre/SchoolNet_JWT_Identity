using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNet_JWT_Identity.Entities;
using SchoolNet_JWT_Identity.Entities.Base;

namespace SchoolNet_JWT_Identity.Context.Mappings
{
    public class StudentClassMapper : IEntityTypeConfiguration<StudentClass>
    {
        public void Configure(EntityTypeBuilder<StudentClass> builder)
        {
            builder.ToTable("StudentClass");

            builder.HasOne(sc => sc.Course)
                .WithMany(c => c.StudentClasses)
                .HasForeignKey(sc => sc.CourseId);

            builder.HasOne(sc => sc.Student)
                .WithMany(c => c.StudentClasses)
                .HasForeignKey(sc => sc.StudentId);

            builder.HasOne(sc => sc.Teacher)
                .WithMany(c => c.StudentClasses)
                .HasForeignKey(sc => sc.TeacherId);

            builder.OwnsOne<Audit>(c => c.Audit, a =>
            {
                a.Property(c => c.CreationDate)
                    .HasColumnType("datetime")
                    .IsRequired();

                a.Property(c => c.LastModificationDate)
                    .HasColumnType("datetime")
                    .IsRequired();

                a.Property(c => c.CreationUserId)
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                a.Property(c => c.LastModifierUserId)
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();
            });
        }
    }
}
