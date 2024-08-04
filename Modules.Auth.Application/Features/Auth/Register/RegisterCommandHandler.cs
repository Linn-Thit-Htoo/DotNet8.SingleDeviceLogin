using MediatR;
using Shared.DTOs.Features.Auth;
using Shared.DTOs.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modules.Auth.Domain.Interfaces;
using Modules.Auth.Application.Services;
using Shared.Domain.Enums;

namespace Modules.Auth.Application.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponseModel>>
    {
        private readonly IAuthService _authService;
        private readonly RegisterValidator _registerValidator;

        public RegisterCommandHandler(IAuthService authService, RegisterValidator registerValidator)
        {
            _authService = authService;
            _registerValidator = registerValidator;
        }

        public async Task<Result<RegisterResponseModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Result<RegisterResponseModel> responseModel;
            try
            {
                var validationResult = await _registerValidator.ValidateAsync(request.RequestModel);
                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ErrorMessage));
                    responseModel = Result<RegisterResponseModel>.FailureResult(errors);
                    goto result;
                }

                if (!Enum.IsDefined(typeof(EnumUserRole), request.RequestModel.UserRole))
                {
                    responseModel = Result<RegisterResponseModel>.FailureResult("Invalid User Role.");
                    goto result;
                }

                responseModel = await _authService.Register(request.RequestModel, cancellationToken);
            }
            catch (Exception ex)
            {
                responseModel = Result<RegisterResponseModel>.FailureResult(ex);
            }

        result:
            return responseModel;
        }
    }
}
