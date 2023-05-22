using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperCesiApi.Models;

namespace SuperCesiApi.Data;

public class SuperCesiApiDbContext : IdentityDbContext
{
    public SuperCesiApiDbContext(DbContextOptions<SuperCesiApiDbContext> options)
        : base(options)
    {
    }

    public DbSet<SuperHero> SuperHeroes { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<IncidentType> IncidentTypes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<SuperHero>(entity =>
        {
            entity.ToTable(nameof(SuperHero));
            entity.HasKey(e => e.Id);
            entity.HasMany(s => s.IncidentTypes)
                .WithMany(i => i.SuperHeroes)
                .UsingEntity<Dictionary<string, object>>(
                    "SuperHeroIncidentType",
                    j => j
                        .HasOne<IncidentType>()
                        .WithMany()
                        .HasForeignKey("IncidentTypeId")
                        .HasConstraintName("FK_SuperHeroIncidentType_IncidentTypes_IncidentTypeId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<SuperHero>()
                        .WithMany()
                        .HasForeignKey("SuperHeroId")
                        .HasConstraintName("FK_SuperHeroIncidentType_SuperHeroes_SuperHeroId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("SuperHeroId", "IncidentTypeId");
                        j.ToTable("SuperHeroIncidentType");
                    }
                );
        });

        builder.Entity<Incident>(entity =>
        {
            entity.ToTable(nameof(Incident));
            entity.HasKey(k => k.Id);
            
            entity.HasOne(i => i.IncidentType)
                .WithMany()
                .HasForeignKey(i => i.IncidentTypeId);
        });
        
        builder.Entity<IdentityUser>(b =>
        {
            // Primary key
            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            b.ToTable("Users");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            b.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<IdentityUserToken<string>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        });

        builder.Entity<IdentityUserClaim<string>>(b =>
        {
            // Primary key
            b.HasKey(uc => uc.Id);

            // Maps to the UserClaims table
            b.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<string>>(b =>
        {
            // Composite primary key consisting of the LoginProvider and the key to use
            // with that provider
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(l => l.LoginProvider).HasMaxLength(128);
            b.Property(l => l.ProviderKey).HasMaxLength(128);

            // Maps to the UserLogins table
            b.ToTable("UserLogins");
        });

        builder.Entity<IdentityUserToken<string>>(b =>
        {
            // Composite primary key consisting of the UserId, LoginProvider and Name
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(t => t.LoginProvider).HasMaxLength(256);
            b.Property(t => t.Name).HasMaxLength(256);

            // Maps to the UserTokens table
            b.ToTable("UserTokens");
        });

        builder.Entity<IdentityRole>(b =>
        {
            // Primary key
            b.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the Roles table
            b.ToTable("Roles");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.Name).HasMaxLength(256);
            b.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        });

        builder.Entity<IdentityRoleClaim<string>>(b =>
        {
            // Primary key
            b.HasKey(rc => rc.Id);

            // Maps to the RoleClaims table
            b.ToTable("RoleClaims");
        });

        builder.Entity<IdentityUserRole<string>>(b =>
        {
            // Primary key
            b.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the UserRoles table
            b.ToTable("UserRoles");
        });
    }
}