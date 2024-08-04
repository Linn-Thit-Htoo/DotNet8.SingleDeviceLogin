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

        public async Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel, CancellationToken cancellationToken)
        {
            Result<JwtResponseModel> responseModel;
            try
            {
                var oldSession = await _context.Tbl_Logins
                    .FirstOrDefaultAsync(x => x.Email == requestModel.Email, cancellationToken);
                if (oldSession is not null)
                {
                    _context.Tbl_Logins.Remove(oldSession);
                }

                var item = await _context.Tbl_Users
                    .FirstOrDefaultAsync(x => x.Email == requestModel.Email && x.Password == requestModel.Password && x.IsActive
                    , cancellationToken);
                if (item is null)
                {
                    responseModel = Result<JwtResponseModel>.NotFoundResult("User Not Found");
                    goto result;
                }

                string token = "Sample Token";
                await _context.Tbl_Logins.AddAsync(requestModel.Map(token), cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var model = new JwtResponseModel()
                {
                    UserId = item.UserId,
                    Email = item.Email,
                    UserName = item.UserName,
                    Token = token
                };
                responseModel = Result<JwtResponseModel>.SuccessResult(model);
            }
            catch (Exception ex)
            {
                responseModel = Result<JwtResponseModel>.FailureResult(ex);
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
