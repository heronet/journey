using journey.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace journey.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    public required DbSet<Hotel> Hotels { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "4bd98ac5-7a27-4094-93f9-b18545a19f1b", Name = "Member", NormalizedName = "MEMBER" },
            new IdentityRole { Id = "115dd51e-07e2-42cb-8191-0a76a814595c", Name = "Moderator", NormalizedName = "MODERATOR" },
            new IdentityRole { Id = "6fe6317b-cdd7-474e-9f5d-92bbcfee656a", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "453135bf-9d7f-4f9f-b93e-01466bf7ab77", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" }
        );
    }

}
