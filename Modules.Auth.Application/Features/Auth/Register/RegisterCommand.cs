namespace Modules.Auth.Application.Features.Auth.Register;

public class RegisterCommand : IRequest<Result<RegisterResponseModel>>
{
    public RegisterRequestModel RequestModel { get; set; }

    public RegisterCommand(RegisterRequestModel requestModel)
    {
        RequestModel = requestModel;
    }
}
