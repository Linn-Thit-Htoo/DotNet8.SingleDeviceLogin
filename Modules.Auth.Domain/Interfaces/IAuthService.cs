namespace Modules.Auth.Domain.Interfaces;

public interface IAuthService
{
    Task<Result<RegisterResponseModel>> Register(
        RegisterRequestModel requestModel,
        CancellationToken cancellationToken
    );
    Task<Result<JwtResponseModel>> Login(
        LoginRequestModel requestModel,
        CancellationToken cancellationToken
    );
    Task<Result<UserListResponseModel>> GetUserList(CancellationToken cancellationToken);
}
