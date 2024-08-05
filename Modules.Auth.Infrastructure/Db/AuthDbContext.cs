namespace Modules.Auth.Infrastructure.Db;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Tbl_User> Tbl_Users { get; set; }
    public DbSet<Tbl_Login> Tbl_Logins { get; set; }
}
