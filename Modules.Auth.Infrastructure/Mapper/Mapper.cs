namespace Modules.Auth.Infrastructure.Mapper;

public static class Mapper
{
    public static Tbl_User Map(this RegisterRequestModel requestModel)
    {
        return new Tbl_User
        {
            UserId = Ulid.NewUlid().ToString(),
            UserName = requestModel.UserName,
            Email = requestModel.Email,
            Password = requestModel.Password,
            UserRole = requestModel.UserRole,
            IsActive = true
        };
    }

    public static Tbl_Login Map(this LoginRequestModel requestModel, string token)
    {
        return new Tbl_Login
        {
            LoginId = Ulid.NewUlid().ToString(),
            Email = requestModel.Email,
            Token = token,
            CreatedDate = DevCode.GetCurrentMyanmarDateTime()
        };
    }
}
