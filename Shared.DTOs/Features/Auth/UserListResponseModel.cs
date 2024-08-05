namespace Shared.DTOs.Features.Auth;

public class UserListResponseModel
{
    public List<UserModel> DataLst { get; set; }

    public UserListResponseModel(List<UserModel> dataLst)
    {
        DataLst = dataLst;
    }
}
