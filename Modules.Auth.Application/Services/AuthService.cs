using Microsoft.EntityFrameworkCore;
using Modules.Auth.Domain.Interfaces;
using Modules.Auth.Infrastructure.Db;
using Modules.Auth.Infrastructure.Mapper;
using Shared.DTOs.Features;
using Shared.DTOs.Features.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _context;

        public AuthService(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Result<RegisterResponseModel>> Register(RegisterRequestModel requestModel, CancellationToken cancellationToken)
        {
            Result<RegisterResponseModel> responseModel;
            try
            {
                bool emailDuplicate = await IsEmailDuplicate(requestModel.Email);
                if (emailDuplicate)
                {
                    responseModel = Result<RegisterResponseModel>.DuplicateResult("User with this email already exists. Please login.");
                    goto result;
                }

                await _context.Tbl_Users.AddAsync(requestModel.Map());
                await _context.SaveChangesAsync(cancellationToken);

                responseModel = Result<RegisterResponseModel>.SuccessResult();
            }
            catch (Exception ex)
            {
                responseModel = Result<RegisterResponseModel>.FailureResult(ex);
            }

        result:
            return responseModel;
        }

        private async Task<bool> IsEmailDuplicate(string email)
        {
            return await _context.Tbl_Users.AnyAsync(x => x.Email == email && x.IsActive);
        }
    }
}
