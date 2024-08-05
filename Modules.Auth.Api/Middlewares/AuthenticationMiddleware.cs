using Microsoft.EntityFrameworkCore;
using Modules.Auth.Domain.Enums;
using Modules.Auth.Infrastructure.Db;
using Shared.Domain.Enums;
using Shared.Domain.Resources;
using Shared.DTOs.Features;

namespace Modules.Auth.Api.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly TokenValidationService _tokenValidationService;
        private readonly RequestDelegate _next;
        private readonly AuthDbContext _context;
        private readonly AesService _aesService;

        public AuthenticationMiddleware(TokenValidationService tokenValidationService, RequestDelegate next, AuthDbContext context, AesService aesService)
        {
            _tokenValidationService = tokenValidationService;
            _next = next;
            _context = context;
            _aesService = aesService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Result<string> responseModel;
            try
            {
                string? authHeader = context.Request.Headers["Authorization"];
                string requestPath = context.Request.Path;

                if (ShouldPass(requestPath))
                {
                    await _next(context);
                    return;
                }

                if (authHeader is not null && authHeader.StartsWith("Bearer"))
                {
                    string[] header_token = authHeader.Split(" ");
                    string header = header_token[0];
                    string token = header_token[1];
                    var principal = _tokenValidationService.ValidateToken(token);

                    if (principal is null)
                    {
                        responseModel = GetUnAuthorizedResult();
                        await context.Response.WriteAsync(responseModel.SerializeObject());
                        return;
                    }

                    string encryptedEmail = principal.FindFirst("Email")!.Value;
                    string decryptedEmail = _aesService.DecryptString(encryptedEmail);

                    var oldSession = await _context.Tbl_Logins
                        .FirstOrDefaultAsync(x => x.Email == decryptedEmail
                        && x.Token == token);

                    if (oldSession is null)
                    {
                        responseModel = GetSessionExpiredResult();
                        await context.Response.WriteAsync(responseModel.SerializeObject());
                        return;
                    }

                    await _next.Invoke(context);
                }
                else
                {
                    responseModel = GetUnAuthorizedResult();
                    await context.Response.WriteAsync(responseModel.SerializeObject());
                    return;
                }
            }
            catch (Exception ex)
            {
                responseModel = GetUnAuthorizedResult();
                await context.Response.WriteAsync(responseModel.SerializeObject());
            }
        }

        private bool ShouldPass(string requestPath)
        {
            return requestPath == "/api/account/login" || requestPath == "/api/account/register";
        }

        private Result<string> GetUnAuthorizedResult() =>
            Result<string>.FailureResult(MessageResource.Unauthorized, EnumStatusCode.UnAuthorized);

        private Result<string> GetSessionExpiredResult() =>
            Result<string>.FailureResult("Session Expired.", EnumStatusCode.UnAuthorized);
    }
}
