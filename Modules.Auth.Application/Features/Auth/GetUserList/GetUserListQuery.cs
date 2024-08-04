using MediatR;
using Shared.DTOs.Features.Auth;
using Shared.DTOs.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Application.Features.Auth.GetUserList
{
    public class GetUserListQuery : IRequest<Result<UserListResponseModel>>
    {
    }
}
