﻿using Shared.DTOs.Features;
using Shared.DTOs.Features.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResponseModel>> Register(RegisterRequestModel requestModel);
    }
}
