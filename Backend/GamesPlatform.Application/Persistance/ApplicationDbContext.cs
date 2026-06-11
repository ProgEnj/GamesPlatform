using GamesPlatform.Application.Persistance.Identity;
using GamesPlatform.Core.Model;
using GamesPlatform.Core.Model.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamesPlatform.Application.Persistance;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
{
    
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<DomainGame> Games { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // builder.Entity<IdentityRole>().HasData([new IdentityRole("Admin")]);
    }
    
}