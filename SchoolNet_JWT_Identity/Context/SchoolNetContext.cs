using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolNet_JWT_Identity.ApplicationUserSetup;
using SchoolNet_JWT_Identity.Context.Mappings;
using SchoolNet_JWT_Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolNet_JWT_Identity.Context
{
    public class SchoolNetContext : IdentityDbContext<User>
    {
        private readonly IApplicationUser _applicationUser;

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<User> Users { get; set; }

        public SchoolNetContext(DbContextOptions<SchoolNetContext> options,
                                IApplicationUser applicationUser)
            :base(options)
        {
            _applicationUser = applicationUser;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StudentMapper());
            modelBuilder.ApplyConfiguration(new CourseMapper());
            modelBuilder.ApplyConfiguration(new TeacherMapper());
            modelBuilder.ApplyConfiguration(new StudentClassMapper());

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker.Entries();

            var auditedEntities = new List<string>
            {
                "Course",
                "Student",
                "Teacher",
                "StudentClass"                
            };

            var fullAuditedEntries = entries.Where(entry => (entry.Metadata.ClrType.BaseType != null &&
                                     auditedEntities.Contains(entry.Metadata.ClrType.BaseType.Name)) ||
                                     (entry.Metadata.ClrType.BaseType.BaseType != null &&
                                     auditedEntities.Contains(entry.Metadata.ClrType.BaseType.BaseType.Name)));

            foreach (EntityEntry entityEntry in fullAuditedEntries)
            {
                var entity = entityEntry.Entity;
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entity.GetType().GetProperty("CreationTime").SetValue(entity, DateTime.Now, null);
                        entity.GetType().GetProperty("LastModificationTime").SetValue(entity, DateTime.Now, null);
                        entity.GetType().GetProperty("CreatorUserId").SetValue(entity, _applicationUser.UserId, null);
                        break;
                    case EntityState.Modified:
                        entityEntry.Property("CreationTime").IsModified = false;
                        entityEntry.Property("CreatorUserId").IsModified = false;

                        entity.GetType().GetProperty("LastModificationTime").SetValue(entity, DateTime.Now, null);
                        entity.GetType().GetProperty("LastModifierUserId").SetValue(entity, _applicationUser.UserId, null);
                        break;
                    case EntityState.Deleted:                        
                        break;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }        
    }
}
