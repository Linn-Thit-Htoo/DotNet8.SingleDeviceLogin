namespace Modules.Auth.Domain.Enums;

public enum EnumAuthEndpoints
{
    [Description("/api/account/login")]
    Login,

    [Description("/api/account/register")]
    Register
}
