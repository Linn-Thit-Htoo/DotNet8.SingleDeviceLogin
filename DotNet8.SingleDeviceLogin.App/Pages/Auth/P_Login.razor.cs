using DotNet8.SingleDeviceLogin.App.Services;
using Shared.DTOs.Features;
using Shared.DTOs.Features.Auth;

namespace DotNet8.SingleDeviceLogin.App.Pages.Auth
{
	public partial class P_Login
	{
		private LoginRequestModel RequestModel = new();

		private async Task Login()
		{
			var responseModel = await HttpClientService.ExecuteAsync<Result<JwtResponseModel>>("/api/account/login", EnumHttpMethod.POST, RequestModel);

			if (responseModel.IsSuccess)
			{
				await LocalStorage.SetItemAsStringAsync("token", responseModel.Token);
			}
		}
	}
}
