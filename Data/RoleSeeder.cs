using Microsoft.AspNetCore.Identity;
using SuperCesiApi.Data;

namespace SuperCesiApi.Models;

/// <summary>
/// A seeder class to populate the roles in the database.
/// </summary>
public class RoleSeeder
{
    /// <summary>
    /// Seeds the roles in the specified database context.
    /// </summary>
    /// <param name="context">The database context.</param>
    public static void Seed(SuperCesiApiDbContext context)
    {
        // Check if incidents already exist in the database
        if (context.Roles.Any())
        {
            return; // Data already seeded
        }

        // Create and add incidents
        var roles = new[]
        {
            new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole{ Name = "CityUser", NormalizedName = "CITYUSER", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole{ Name = "SuperHero", NormalizedName = "SUPERHERO", ConcurrencyStamp = Guid.NewGuid().ToString() }
        };

        context.Roles.AddRange(roles);
        context.SaveChanges();
    }
}