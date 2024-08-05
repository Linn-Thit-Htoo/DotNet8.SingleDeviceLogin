namespace Modules.Auth.Domain.Entities;

[Table("Tbl_Login")]
public class Tbl_Login
{
    [Key]
    public string LoginId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string CreatedDate { get; set; }
}
