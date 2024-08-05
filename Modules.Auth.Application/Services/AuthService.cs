namespace Modules.Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly AuthDbContext _context;
    private readonly AesService _aesService;
    private readonly JWTAuth _jwtAuth;

    public AuthService(AuthDbContext context, AesService aesService, JWTAuth jwtAuth)
    {
        _context = context;
        _aesService = aesService;
        _jwtAuth = jwtAuth;
    }

    public async Task<Result<UserListResponseModel>> GetUserList(
        CancellationToken cancellationToken
    )
    {
        Result<UserListResponseModel> responseModel;
        try
        {
            var lst = await _context
                .Tbl_Users.OrderByDescending(x => x.UserId)
                .Where(x => x.IsActive)
                .ToListAsync();

            var userLst = lst.Select(x => new UserModel
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    Email = x.Email,
                    UserRole = x.UserRole
                })
                .ToList();

            responseModel = Result<UserListResponseModel>.SuccessResult(
                data: new UserListResponseModel(userLst)
            );
        }
        catch (Exception ex)
        {
            responseModel = Result<UserListResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    public async Task<Result<RegisterResponseModel>> Register(
        RegisterRequestModel requestModel,
        CancellationToken cancellationToken
    )
    {
        Result<RegisterResponseModel> responseModel;
        try
        {
            bool emailDuplicate = await IsEmailDuplicate(requestModel.Email);
            if (emailDuplicate)
            {
                responseModel = Result<RegisterResponseModel>.DuplicateResult(
                    "User with this email already exists. Please login."
                );
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

    public async Task<Result<JwtResponseModel>> Login(
        LoginRequestModel requestModel,
        CancellationToken cancellationToken
    )
    {
        Result<JwtResponseModel> responseModel;
        try
        {
            var oldSession = await _context.Tbl_Logins.FirstOrDefaultAsync(
                x => x.Email == requestModel.Email,
                cancellationToken
            );
            if (oldSession is not null)
            {
                _context.Tbl_Logins.Remove(oldSession);
            }

            var item = await _context.Tbl_Users.FirstOrDefaultAsync(
                x =>
                    x.Email == requestModel.Email
                    && x.Password == requestModel.Password
                    && x.IsActive,
                cancellationToken
            );
            if (item is null)
            {
                responseModel = Result<JwtResponseModel>.NotFoundResult("User Not Found");
                goto result;
            }

            var model = new JwtResponseModel()
            {
                UserId = _aesService.EncryptString(item.UserId),
                Email = _aesService.EncryptString(item.Email),
                UserName = _aesService.EncryptString(item.UserName)
            };
            var token = _jwtAuth.GetJWTToken(model);

            await _context.Tbl_Logins.AddAsync(requestModel.Map(token), cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            responseModel = Result<JwtResponseModel>.SuccessResult(token, model);
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
