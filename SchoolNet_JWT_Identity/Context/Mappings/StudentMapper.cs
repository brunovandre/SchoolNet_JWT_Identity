using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNet_JWT_Identity.Entities;
using SchoolNet_JWT_Identity.Entities.Base;

namespace SchoolNet_JWT_Identity.Context.Mappings
{
    public class StudentMapper : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasMany(c => c.StudentClasses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId);

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
