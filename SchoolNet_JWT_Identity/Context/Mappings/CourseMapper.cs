using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNet_JWT_Identity.Entities;
using SchoolNet_JWT_Identity.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolNet_JWT_Identity.Context.Mappings
{
    public class CourseMapper : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");

            builder.HasMany(c => c.StudentClasses)
                .WithOne(sc => sc.Course)
                .HasForeignKey(sc => sc.CourseId);

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
