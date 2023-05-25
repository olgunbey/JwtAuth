using JsonWebToken.Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Repository.Context
{
    public class AppDbContext:IdentityDbContext<UserApp,UserRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptionsBuilder):base(dbContextOptionsBuilder)
        {
        }
        public DbSet<UserRefleshToken> UserRefleshTokens { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
