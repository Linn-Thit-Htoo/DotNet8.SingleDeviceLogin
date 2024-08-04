using Shared.DTOs.Features;
using Shared.DTOs.Features.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Result<RegisterResponseModel>> Register(RegisterRequestModel requestModel, CancellationToken cancellationToken);
        Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel, CancellationToken cancellationToken);
        Task<Result<UserListResponseModel>> GetUserList(CancellationToken cancellationToken);
    }
}
