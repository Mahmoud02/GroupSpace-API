using Accounts.GroupSpace.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Accounts.GroupSpace.DbContexts
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }
        //Used To Hash Password
        private readonly IPasswordHasher<User> _passwordHasher;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options , IPasswordHasher<User> passwordHasher) : base(options)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Subject)
            .IsUnique();

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
           
            var AdminUser = new User()
            {
                Id = new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),           
                Subject = "d860efca-22d9-47fd-8249-791ba61b07c7",
                Username = "Mahmoud",
                Email = "test@yahoo.com",
                Active = true
            };
            AdminUser.Password = _passwordHasher.HashPassword(AdminUser, "password");
            
            modelBuilder.Entity<User>().HasData(AdminUser);
               

            modelBuilder.Entity<UserClaim>().HasData(
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                 Type = JwtClaimTypes.Role,
                 Value = "Admin"
             });

        }

    
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // get updated entries
            var updatedConcurrencyAwareEntries = ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Modified)
                    .OfType<IConcurrencyAware>();

            foreach (var entry in updatedConcurrencyAwareEntries)
            {
                entry.ConcurrencyStamp = Guid.NewGuid().ToString();
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
