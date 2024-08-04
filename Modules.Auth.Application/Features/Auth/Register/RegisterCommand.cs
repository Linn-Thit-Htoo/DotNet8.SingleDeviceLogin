using MediatR;
using Shared.DTOs.Features;
using Shared.DTOs.Features.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Application.Features.Auth.Register
{
    public class RegisterCommand : IRequest<Result<RegisterResponseModel>>
    {
        public RegisterRequestModel RequestModel { get; set; }

        public RegisterCommand(RegisterRequestModel requestModel)
        {
            RequestModel = requestModel;
        }
    }
}
