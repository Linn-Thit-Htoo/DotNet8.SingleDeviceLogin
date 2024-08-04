using Microsoft.EntityFrameworkCore;
using Modules.Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Infrastructure.Db
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tbl_User> Tbl_Users { get; set; }
    }
}
