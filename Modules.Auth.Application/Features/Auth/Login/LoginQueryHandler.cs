namespace Modules.Auth.Application.Features.Auth.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<JwtResponseModel>>
{
    private readonly IAuthService _authService;
    private readonly LoginValidator _loginValidator;

    public LoginQueryHandler(IAuthService authService, LoginValidator loginValidator)
    {
        _authService = authService;
        _loginValidator = loginValidator;
    }

    public async Task<Result<JwtResponseModel>> Handle(
        LoginQuery request,
        CancellationToken cancellationToken
    )
    {
        Result<JwtResponseModel> responseModel;
        try
        {
            var validationResult = await _loginValidator.ValidateAsync(request.RequestModel);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(
                    " ",
                    validationResult.Errors.Select(x => x.ErrorMessage)
                );
                responseModel = Result<JwtResponseModel>.FailureResult(errors);
                goto result;
            }

            responseModel = await _authService.Login(request.RequestModel, cancellationToken);
        }
        catch (Exception ex)
        {
            responseModel = Result<JwtResponseModel>.FailureResult(ex);
        }

        result:
        return responseModel;
    }
}
