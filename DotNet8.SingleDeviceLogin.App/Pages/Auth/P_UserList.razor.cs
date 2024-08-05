using DotNet8.SingleDeviceLogin.App.Services;
using MudBlazor;
using Shared.Domain.Enums;
using Shared.DTOs.Features;
using Shared.DTOs.Features.Auth;

namespace DotNet8.SingleDeviceLogin.App.Pages.Auth
{
    public partial class P_UserList
    {
        private Result<UserListResponseModel> ResponseModel = new();

        protected override async Task OnInitializedAsync()
        {
            await List();
        }

        private async Task List()
        {
            ResponseModel = await HttpClientService.ExecuteAsync<Result<UserListResponseModel>>(Endpoints.UserList, EnumHttpMethod.GET);
            if (ResponseModel.IsError)
            {
                Snackbar.Add(ResponseModel.Message, Severity.Error);
            }

            if (ResponseModel.StatusCode == EnumStatusCode.UnAuthorized)
            {
                Navigation.NavigateTo("/");
            }
        }
    }
}
