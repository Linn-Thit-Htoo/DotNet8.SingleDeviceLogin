namespace Modules.Auth.Domain.Entities;

[Table("Tbl_User")]
public class Tbl_User
{
    [Key]
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserRole { get; set; }
    public bool IsActive { get; set; }
}
