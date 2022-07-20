using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerceApp.Models;

namespace SimpleEcommerceApp.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Tbl_Category { get; set; }

        public DbSet<ApplicationType> Tbl_ApplicationType { get; set; }

        public DbSet<Product> Tbl_Product { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
