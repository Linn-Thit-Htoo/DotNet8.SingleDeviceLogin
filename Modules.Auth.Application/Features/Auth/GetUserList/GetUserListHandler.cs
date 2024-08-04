using MediatR;
using Shared.DTOs.Features.Auth;
using Shared.DTOs.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modules.Auth.Domain.Interfaces;

namespace Modules.Auth.Application.Features.Auth.GetUserList
{
    public class GetUserListHandler : IRequestHandler<GetUserListQuery, Result<UserListResponseModel>>
    {
        private readonly IAuthService _authService;

        public GetUserListHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<UserListResponseModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _authService.GetUserList(cancellationToken);
        }
    }
}
