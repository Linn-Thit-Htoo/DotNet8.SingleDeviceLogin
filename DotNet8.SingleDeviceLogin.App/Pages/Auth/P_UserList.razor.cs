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
