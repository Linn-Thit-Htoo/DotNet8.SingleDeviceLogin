using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Application.Features.Auth.Login
{
    public class LoginQuery : IRequest<Result<JwtResponseModel>>
    {
        public LoginRequestModel RequestModel { get; set; }

        public LoginQuery(LoginRequestModel requestModel)
        {
            RequestModel = requestModel;
        }
    }
}
