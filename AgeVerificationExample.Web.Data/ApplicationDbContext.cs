using AgeVerificationExample.Web.Contracts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AgeVerificationExample.Web.Data
{
    /// <summary>
    /// The entity framework/identity framework database context.
    /// Should not need to use this directly. Use <see cref="IApplicationUserContext"/> or other bounded contexts instead.
    /// </summary>
    public class ApplicationDbContext : 
        IdentityDbContext<
            ApplicationUser,
            ApplicationUserIdentityRole, 
            Guid,
            ApplicationUserIdentityUserClaim,
            ApplicationUserIdentityUserRole,
            ApplicationUserIdentityUserLogin,
            ApplicationUserIdentityRoleClaim,
            ApplicationUserIdentityUserToken>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Define the model.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // If database schema gets too big consider moving these mapping methods to their own context
            // specific class using the ModelSnapshot base class
            MapApplicationUsers(modelBuilder.Entity<ApplicationUser>());
            MapIdentityFrameworkTypes(modelBuilder);
            MapRegistrationAttempts(modelBuilder.Entity<RegistrationAttempt>());
            MapLoginAttempts(modelBuilder.Entity<LoginAttempt>());
        }

        private void MapLoginAttempts(EntityTypeBuilder<LoginAttempt> entityBuilder)
        {
            entityBuilder.ToTable("LoginAttempts");

            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.AttemptDate);
            entityBuilder.Property(e => e.Status);

            // Foreign key reference to ApplicationUser
            entityBuilder
                .HasOne(e => e.User)
                .WithMany(c => c.LoginAttempts)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .HasConstraintName("FK_LoginAttempts_UserId");
        }

        private void MapRegistrationAttempts(EntityTypeBuilder<RegistrationAttempt> entityBuilder)
        {
            entityBuilder.ToTable("RegistrationAttempts");

            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Name).IsRequired().HasMaxLength(256);
            entityBuilder.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entityBuilder.Property(e => e.LastAttempt);
            entityBuilder.Property(e => e.Failures);
            entityBuilder.Property(e => e.LockedOutDate);

            entityBuilder.HasIndex(e => new { e.Email, e.Name })
                .IsUnique()
                .HasName("RegistrationAttempts_EmailNameIndex");
        }

        private void MapApplicationUsers(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.ToTable("ApplicationUsers");

            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.AccessFailedCount);
            entityBuilder.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
            entityBuilder.Property(e => e.DateOfBirth);
            entityBuilder.Property(e => e.Email).HasMaxLength(256);
            entityBuilder.Property(e => e.EmailConfirmed);
            entityBuilder.Property(e => e.LockoutEnabled);
            entityBuilder.Property(e => e.LockoutEnd);
            entityBuilder.Property(e => e.Name).IsRequired().HasMaxLength(256);
            entityBuilder.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entityBuilder.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entityBuilder.Property(e => e.PasswordHash);
            entityBuilder.Property(e => e.PhoneNumber);
            entityBuilder.Property(e => e.PhoneNumberConfirmed);
            entityBuilder.Property(e => e.SecurityStamp);
            entityBuilder.Property(e => e.TwoFactorEnabled);
            entityBuilder.Property(e => e.UserName).HasMaxLength(256);

            entityBuilder.HasIndex(e => e.NormalizedEmail).HasName("ApplicationUsers_EmaiIndex");
            entityBuilder.HasIndex(e => e.NormalizedUserName)
                .IsUnique()
                .HasName("ApplicationUsers_UserNameIndex")
                .HasFilter("[NormalizedUserName] IS NOT NULL");
        }

        private void MapIdentityFrameworkTypes(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<ApplicationUserIdentityRole>(b =>
           {
               b.HasKey(e => e.Id);
               b.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
               b.Property(e => e.Name).HasMaxLength(256);
               b.Property(e => e.NormalizedName).HasMaxLength(256);
               b.HasIndex(e => e.NormalizedName).IsUnique().HasName("ApplicationUserIdentityRole_NormalizedName").HasFilter("[NormalizedName] IS NOT NULL");
               b.ToTable("ApplicationUserIdentityRoles");
           });

            modelBuilder.Entity<ApplicationUserIdentityRoleClaim>(b =>
            {
                b.Property(e => e.Id).ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                b.HasKey(e => e.Id);
                b.Property(e => e.ClaimType);
                b.Property(e => e.ClaimValue);
                b.Property(e => e.RoleId);
                b.HasIndex(e => e.RoleId).HasName("ApplicationUserIdentityRoleClaims_RoleId");

                b.HasOne(e => e.Role)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.ToTable("ApplicationUserRoleClaims");
            });

            modelBuilder.Entity<ApplicationUserIdentityUserClaim>(b =>
            {
                b.Property(e => e.Id).ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                b.HasKey(e => e.Id);
                b.Property(e => e.ClaimType);
                b.Property(e => e.ClaimValue);
                b.Property(e => e.UserId);
                b.HasIndex(e => e.UserId).HasName("ApplicationUserIdentityClaims_UserId");

                b.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.ToTable("ApplicationUserClaims");
            });

            modelBuilder.Entity<ApplicationUserIdentityUserLogin>(b =>
            {
                b.Property(e => e.LoginProvider);                
                b.Property(e => e.ProviderKey);
                b.Property(e => e.ProviderDisplayName);
                b.Property(e => e.UserId);

                b.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                b.HasIndex(e => e.UserId).HasName("ApplicationUserIdentityUserLogin_UserId");

                b.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.ToTable("ApplicationUserIdentityUserLogins");
            });

            modelBuilder.Entity<ApplicationUserIdentityUserRole>(b =>
            {
                b.Property(e => e.UserId);
                b.Property(e => e.RoleId);
                b.Property(e => e.UserId);

                b.HasKey(e => new { e.UserId, e.RoleId });
                b.HasIndex(e => e.RoleId).HasName("ApplicationUserIdentityUserRole_RoleId");

                b.HasOne(e => e.Role)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.ToTable("ApplicationUserIdentityUserRoles");
            });

            modelBuilder.Entity<ApplicationUserIdentityUserToken>(b =>
            {
                b.Property(e => e.UserId);
                b.Property(e => e.LoginProvider);
                b.Property(e => e.Name);
                b.Property(e => e.Value);
                b.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                b.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.ToTable("ApplicationUserIdentityUserTokens");
            });
        }
    }
}