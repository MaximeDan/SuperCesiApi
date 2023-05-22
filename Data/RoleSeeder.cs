using Microsoft.AspNetCore.Identity;
using SuperCesiApi.Data;

namespace SuperCesiApi.Models;

/// <summary>
/// 
/// </summary>
public class RoleSeeder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
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