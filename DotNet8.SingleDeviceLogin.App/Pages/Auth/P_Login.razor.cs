namespace DotNet8.SingleDeviceLogin.App.Pages.Auth;

public partial class P_Login
{
	private LoginRequestModel RequestModel = new();

	private async Task Login()
	{
		var responseModel = await HttpClientService.ExecuteAsync<Result<JwtResponseModel>>(Endpoints.Login, EnumHttpMethod.POST, RequestModel);

		if (responseModel.IsSuccess)
		{
			await LocalStorage.SetItemAsStringAsync("token", responseModel.Token);
			Navigation.NavigateTo("/users");
		}
	}
}