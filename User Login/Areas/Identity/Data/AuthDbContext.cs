using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User_Login.Areas.Identity.Data;
using User_Login.Models;

namespace User_Login.Data;

public class AuthDbContext : IdentityDbContext<Users>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {

    }

   public DbSet<Transactions> Transactions { get; set; }
   public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
       // public DbSet<Transactions> Transactions { get; set; }
        //public DbSet<Category> Categories { get; set; }
    }
}
